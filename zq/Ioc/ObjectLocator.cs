using System;

namespace Zq.Ioc
{
    public class ObjectLocator
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
    }
}
