using System;
using StackExchange.Redis;
using Zq.Caching;
using Zq.Ioc;
using Zq.Serializers;

namespace Zq.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private ConnectionMultiplexer _multiplexer;
        protected ConnectionMultiplexer Multiplexer => _multiplexer
                                                       ?? (_multiplexer = ConnectionMultiplexer.Connect("localhost"));


        public T Get<T>(string key, string region = "")
            where T : class
        {
            var db = GetDataBase();
            byte[] value = db.StringGet(MergeKey(key, region));
           
            return ObjectLocator.Resolve<IBinarySerializer>().Deserialize<T>(value);
        }

        public void Set(string key, object data, int cacheTime, string region = "")
        {
            var db = GetDataBase();
            
            var value = ObjectLocator.Resolve<IBinarySerializer>().Serialize(data);
            if (cacheTime > 0)
            {
                db.StringSet(MergeKey(key, region), value, TimeSpan.FromMinutes(cacheTime));
            }
            else
            {
                db.StringSet(MergeKey(key, region), value);
            }
        }

        public bool IsSet(string key, string region = "")
        {
            var db = GetDataBase();
            return db.KeyExists(MergeKey(key, region));
        }

        public void Remove(string key, string region = "")
        {
            try
            {
                var db = GetDataBase();
                db.KeyDelete(MergeKey(key, region));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Clear()
        {
            var endpoints = Multiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = Multiplexer.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        private IDatabase GetDataBase()
        {
            return Multiplexer.GetDatabase();
        }

        private string MergeKey(string key, string region)
        {
            return string.IsNullOrWhiteSpace(region) ? key : $"{key}_{region}";
        }
    }
}
