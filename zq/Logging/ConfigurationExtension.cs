using Zq.Configurations;
using Zq.Ioc;

namespace Zq.Logging
{
    public static class ConfigurationExtension
    {
        public static Configuration UseConsoleLog(this Configuration configuration)
        {
            configuration.Container.Register<ConsoleLogger, ILogger>(LifeTime.Single);
            return configuration;
        }
    }
}
