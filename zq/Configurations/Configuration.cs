using Zq.Ioc;

namespace Zq.Configurations
{
    public class Configuration
    {
        private static Configuration _instance;
        public static Configuration Instance => _instance 
            ?? (_instance = new Configuration());
        public IObjectContainer Container { get; set; }
    }
}
