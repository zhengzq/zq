using Zq.Configurations;
using Zq.DI;

namespace Zq.Unity
{
    public static class ConfigurationExtension
    {
        public static Configuration UseUnity(this Configuration configuration)
        {
            var container = new UnityObjectContainer();

            configuration.Container = container;

            DI.Ioc.SetContainer(container);

            return configuration;
        }
    }
}
