using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Zq.Ioc;

namespace Zq.Autofac
{
    public class AutofacObjectContainer : IObjectContainer
    {
        public IContainer Container { get; private set; }
        public AutofacObjectContainer()
        {
            var builder = new ContainerBuilder();
            Container = builder.Build();
        }
        public Func<ILifetimeScope> CurrentHttpLifetimeScope;
        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        public IEnumerable<T> ResolveAll<T>()
        {
            return Container.Resolve<IEnumerable<T>>();
        }
        public object Resolve(Type type)
        {
            return Container.Resolve(type);
        }
        public IObjectContainer Register<TImplement, TInterface>(LifeTime lifeTime = LifeTime.Single)
            where TInterface : class
            where TImplement : class, TInterface
        {
            var builder = new ContainerBuilder();
            var b = builder.RegisterType<TImplement>().As<TInterface>();
            SwithLifeTime(lifeTime, b);
            builder.Update(Container);
            return this;
        }

        public void CustomRegisterComponents(Action<object> func)
        {
            func(Container);
        }

        public void RegisterComponentFromAssemblys(Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();
            foreach (var type in assemblies.Distinct().SelectMany(asm => asm.GetTypes()))
            {
                var attribute = FindComponentAttribute(type);
                if (attribute != null)
                {
                    var b = builder.RegisterType(type).As(attribute.Type);
                    SwithLifeTime(attribute.LifeTime, b);
                }
            }
            builder.Update(Container);
        }

        public ComponentAttribute FindComponentAttribute(Type t)
        {
            var attrs = t.GetCustomAttributes(typeof(ComponentAttribute), false) as ComponentAttribute[];
            if (attrs != null && attrs.Length == 0)
                return null;
            return attrs?[0];
        }

        private void SwithLifeTime(LifeTime lifeTime,
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> b)
        {
            switch (lifeTime)
            {
                case LifeTime.Single:
                    b.SingleInstance();
                    break;
                case LifeTime.Transient:
                    b.InstancePerDependency();
                    break;
                case LifeTime.Hierarchical:
                default:
                    b.InstancePerLifetimeScope();
                    break;
            }
        }
    }
}
