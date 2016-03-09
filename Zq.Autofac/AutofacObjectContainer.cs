
using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core.Lifetime;
using Zq.Ioc;

namespace Zq.Autofac
{
    public class AutofacObjectContainer : IObjectContainer
    {
        private readonly IContainer _container;
        public AutofacObjectContainer()
        {
            var builder = new ContainerBuilder();
            _container = builder.Build();
        }
        public Func<ILifetimeScope> CurrentHttpLifetimeScope;
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
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
            var b = builder.RegisterType<TImplement>().As<TInterface>();
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
            builder.Update(_container);
            return this;
        }

        public void RegisterComponents(Action<object> func)
        {
            func(_container);
        }

        public void RegisterComponentFromAssemblys(Assembly[] assemblies)
        {
            throw new NotImplementedException();
        }

        //private ILifetimeScope Scope()
        //{
        //    ILifetimeScope scope = null;
        //    scope = CurrentHttpLifetimeScope != null
        //        ? CurrentHttpLifetimeScope()
        //        : this._container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);

        //    return scope;
        //}
    }
}
