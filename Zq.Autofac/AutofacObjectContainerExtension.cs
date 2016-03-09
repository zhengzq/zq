using System;
using Autofac;
using Zq.Ioc;

namespace Zq.Autofac
{
    public static class AutofacObjectContainerExtension
    {
        public static ILifetimeScope Scope(this IObjectContainer container)
        {
            var objectContainer = container as AutofacObjectContainer;
            if (objectContainer != null)
                return objectContainer.Container.BeginLifetimeScope();
            throw new Exception("container not is AutofacObjectContainer");
        }
    }
}
