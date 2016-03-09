using System;

namespace Zq.Ioc
{
    public class ObjectLocator
    {
        private static IObjectContainer _container;


        public static void Create(IObjectContainer container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            EnsureContainer();

            return _container.Resolve<T>();
        }

        private static void EnsureContainer()
        {
            if (_container == null)
                throw new ArgumentNullException("container is not set");
        }
    }
}
