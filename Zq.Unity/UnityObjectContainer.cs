using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Reflection;
using Zq.Ioc;

namespace Zq.Autofac
{
    public class UnityObjectContainer : IObjectContainer
    {
        private readonly IUnityContainer _container;
        public UnityObjectContainer()
        {
            _container = new UnityContainer();
        }
        public IObjectContainer Register<TImplement, TInterface>(LifeTime lifeTime = LifeTime.Single)
            where TImplement : class, TInterface
            where TInterface : class
        {
            switch (lifeTime)
            {
                case LifeTime.Single:
                    _container.RegisterType<TInterface, TImplement>(new ContainerControlledLifetimeManager());
                    break;
                case LifeTime.Transient:
                    _container.RegisterType<TInterface, TImplement>(new TransientLifetimeManager());
                    break;
                case LifeTime.Hierarchical:
                default:
                    _container.RegisterType<TInterface, TImplement>(new HierarchicalLifetimeManager());
                    break;
            }
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

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.Resolve<IEnumerable<T>>();
        }
    }
}
