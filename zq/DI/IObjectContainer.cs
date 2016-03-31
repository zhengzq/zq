using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zq.DI
{
    public interface IObjectContainer
    {
        T Resolve<T>();
        T Resolve<T>(params Tuple<string, object>[] parameters);
        IEnumerable<T> ResolveAll<T>();
        object Resolve(Type type);
        IObjectContainer Register<TImplement, TInterface>(LifeTime lifeTime = LifeTime.Single)
            where TInterface : class
            where TImplement : class, TInterface;

        IObjectContainer Register<TInterface>(object instance, LifeTime lifeTime = LifeTime.Single);

        void CustomRegisterComponents(Action<object> func);
        void RegisterComponentFromAssemblys(Assembly[] assemblies);

    }
}
