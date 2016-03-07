using Zq.Autofac;
using Zq.Configurations;
using Zq.Ioc;

namespace Zq.Unity
{
    public static class ConfigurationExtension
    {
        public static Configuration UseUnity(this Configuration configuration)
        {
            var container = new UnityObjectContainer();

            configuration.Container = container;

            ObjectLocator.Create(container);

            return configuration;
        }
    }
}
