using System;
using Autofac;
using Autofac.Builder;
using Zq.DI;

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

        internal static void SwitchLifetime(this IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> instance, LifeTime lifeTime)
        {
            switch (lifeTime)
            {
                case LifeTime.Single:
                    instance.SingleInstance();
                    break;
                case LifeTime.Transient:
                    instance.InstancePerDependency();
                    break;
                case LifeTime.Thread:
                default:
                    instance.InstancePerLifetimeScope();
                    break;
            }
        }
        internal static void SwitchLifetime<TImplement>(this IRegistrationBuilder<TImplement, ConcreteReflectionActivatorData, SingleRegistrationStyle> instance, LifeTime lifeTime)
        {
            switch (lifeTime)
            {
                case LifeTime.Single:
                    instance.SingleInstance();
                    break;
                case LifeTime.Transient:
                    instance.InstancePerDependency();
                    break;
                case LifeTime.Thread:
                default:
                    instance.InstancePerLifetimeScope();
                    break;
            }
        }

        internal static void SwitchLifetime<TImplement>(this IRegistrationBuilder<TImplement, SimpleActivatorData, SingleRegistrationStyle> instance, LifeTime lifeTime)
        {
            switch (lifeTime)
            {
                case LifeTime.Single:
                    instance.SingleInstance();
                    break;
                case LifeTime.Transient:
                    instance.InstancePerDependency();
                    break;
                case LifeTime.Thread:
                default:
                    instance.InstancePerLifetimeScope();
                    break;
            }
        }

    }
}
