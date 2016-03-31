using Zq.Caching;
using Zq.Configurations;
using Zq.DI;

namespace Zq.Redis
{
    public static class ConfigurationExtension
    {
        public static Configuration UseRedis(this Configuration configuration, string setting = "")
        {
            if (string.IsNullOrWhiteSpace(setting))
            {
                configuration.Container.Register<RedisCacheManager, ICacheManager>(LifeTime.Single);
            }
            else
            {
                configuration.Container.Register<ICacheManager>(new RedisCacheManager(setting));
            }

            return configuration;
        }
    }
}
