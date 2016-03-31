using System;

namespace Zq.DI
{
    public class Ioc
    {
        private static IObjectContainer _container;
        public static void SetContainer(IObjectContainer container)
        {
            _container = container;
        }
        public static T Resolve<T>()
        {
            if (_container == null)
                throw new Exception("container is not set");
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(params Tuple<string, object>[] parameters)
        {
            if (_container == null)
                throw new Exception("container is not set");
            return _container.Resolve<T>(parameters);
        }
    }
}
