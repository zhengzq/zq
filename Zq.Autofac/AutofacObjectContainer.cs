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
        public Func<ILifetimeScope> CurrentHttpLifetimeScope;
        private readonly IContainer _container;
        protected internal IContainer Container { get { return _container; } }
        public AutofacObjectContainer()
        {
            var builder = new ContainerBuilder();
            _container = builder.Build();
        }
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(params Tuple<string, object>[] parameters)
        {
            var namedParameters = new List<NamedParameter>();
            foreach (var parameter in parameters)
            {
                namedParameters.Add(new NamedParameter(parameter.Item1, parameter.Item2));
            }
            return _container.Resolve<T>(namedParameters.ToArray());
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.Resolve<IEnumerable<T>>();
        }
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
        public IObjectContainer Register<TImplement, TInterface>(LifeTime lifeTime = LifeTime.Single)
            where TInterface : class
            where TImplement : class, TInterface
        {
            var builder = new ContainerBuilder();
            var interfacType = typeof(TInterface);
            var implementType = typeof(TImplement);
            
            if (implementType.IsGenericType && interfacType.IsGenericType)
            {
                 builder.RegisterGeneric(implementType).As(interfacType).SwitchLifetime(lifeTime);
            }
            else
            {
                builder.RegisterType<TImplement>().As<TInterface>().SwitchLifetime<TImplement>(lifeTime);
            }

            builder.Update(_container);

            return this;
        }

        public IObjectContainer Register<TInterface>(object instance, LifeTime lifeTime = LifeTime.Single)
        {
            var builder = new ContainerBuilder();
            var interfacType = typeof(TInterface);
            if (interfacType.IsGenericType)
            {
                builder.Register(c => instance).As(interfacType).SwitchLifetime(lifeTime);
            }
            else
            {
                builder.Register(c => instance).As<TInterface>().SwitchLifetime(lifeTime);
            }
            builder.Update(_container);
            return this;
        }

        public void CustomRegisterComponents(Action<object> func)
        {
            func(_container);

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
                    switch (attribute.LifeTime)
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
            builder.Update(_container);
        }
        public ComponentAttribute FindComponentAttribute(Type t)
        {
            var attrs = t.GetCustomAttributes(typeof(ComponentAttribute), false) as ComponentAttribute[];
            if (attrs != null && attrs.Length == 0)
                return null;
            return attrs?[0];
        }

    }


}
