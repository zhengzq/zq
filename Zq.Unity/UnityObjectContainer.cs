using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity;
using Zq.DI;

namespace Zq.Unity
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
                case LifeTime.Thread:
                default:
                    _container.RegisterType<TInterface, TImplement>(new HierarchicalLifetimeManager());
                    break;
            }

            return this;
        }

        public IObjectContainer Register<TInterface>(object instance, LifeTime lifeTime = LifeTime.Single)
        {
            throw new NotImplementedException();
        }

        public void CustomRegisterComponents(Action<object> func)
        {
            throw new NotImplementedException();
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

        public T Resolve<T>(params Tuple<string, object>[] parameters)
        {
            var namedParameters = new List<ResolverOverride>();
            foreach (var parameter in parameters)
            {
                namedParameters.Add(new ParameterOverride(parameter.Item1, parameter.Item2));
            }
            return _container.Resolve<T>(namedParameters.ToArray());

        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.Resolve<IEnumerable<T>>();
        }
    }
}
