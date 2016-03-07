//using System;
//using Zq.Caching;
//using Zq.Configurations;
//using Zq.Ioc;

//namespace Zq.Redis
//{
//    public static class ConfigurationExtension
//    {
//        public static Configuration UseRedis(this Configuration configuration)
//        {
//            configuration.Container.Register<RedisCacheManager, ICacheManager>(LifeTime.Single);
//            return configuration;
//        }
//    }
//}
