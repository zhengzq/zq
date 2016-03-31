using Zq.Configurations;
using Zq.DI;

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
