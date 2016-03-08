using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zq.Ioc
{
    public interface IObjectContainer
    {
        T Resovle<T>();
        IEnumerable<T> ResovleAll<T>();
        object Resovle(Type type);
        IObjectContainer Register<TImplement, TInterface>(LifeTime lifeTime = LifeTime.Single)
            where TInterface : class
            where TImplement : class, TInterface;
        void RegisterComponents(Action<object> func);
        void RegisterComponentFromAssemblys(Assembly[] assemblies);
    }
}
