using System;
using Autofac;
using Zq.Configurations;
using Zq.DI;

namespace Zq.Autofac
{
    public static class ConfigurationExtension
    {
        public static Configuration UseAutofac(this Configuration configuration, Func<ILifetimeScope> currentHttpLifetimeScope = null)
        {
            var container = new AutofacObjectContainer
            {
                CurrentHttpLifetimeScope = currentHttpLifetimeScope
            };

            configuration.Container = container;

            DI.Ioc.SetContainer(container); 

            return configuration;
        }
    }
}
