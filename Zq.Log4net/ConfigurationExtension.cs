using Zq.Configurations;
using Zq.Ioc;
using Zq.Logging;

namespace Zq.Log4net
{
    public static class ConfigurationExtension
    {
        public static Configuration UseLog4Net(this Configuration configuration)
        {
            configuration.Container.Register<Log4NetLogger, ILogger>(LifeTime.Single);
            return configuration;
        }
    }
}
