using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zq.Ioc
{
    public interface IObjectContainer
    {
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
        object Resolve(Type type);
        IObjectContainer Register<TImplement, TInterface>(LifeTime lifeTime = LifeTime.Single)
            where TInterface : class
            where TImplement : class, TInterface;
        void CustomRegisterComponents(Action<object> func);
        void RegisterComponentFromAssemblys(Assembly[] assemblies);

    }
}
