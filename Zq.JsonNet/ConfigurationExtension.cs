using System;
using Zq.Configurations;
using Zq.Ioc;
using Zq.Serializers;

namespace Zq.JsonNet
{
    public static class ConfigurationExtension
    {
        public static Configuration UseJson(this Configuration configuration)
        {
            configuration.Container.Register<JsonSerializer, IJsonSerializer>();

            return configuration;
        }
    }
}
