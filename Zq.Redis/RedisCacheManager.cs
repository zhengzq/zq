using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using StackExchange.Redis;
using Zq.Caching;
using Zq.Ioc;
using Zq.Serializers;

namespace Zq.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        public RedisCacheManager()
        {
            lock (this)
            {
                if (_multiplexer == null)
                    _multiplexer = ConnectionMultiplexer.Connect("localhost");
            }
        }

        private ConnectionMultiplexer _multiplexer;


        public T Get<T>(string key, string region = "")
            where T : class
        {
            var db = GetDataBase();
            byte[] value = db.StringGet(MergeKey(key, region));
            using (var stream = new MemoryStream(value))
            {
                var formatter = new BinaryFormatter();
                var data = formatter.Deserialize(stream);
                return data as T;
            }
        }

        public void Set(string key, object data, int cacheTime, string region = "")
        {
            var db = GetDataBase();

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                var value = stream.ToArray();
                if (cacheTime > 0)
                {
                    db.StringSet(MergeKey(key, region), value, TimeSpan.FromMinutes(cacheTime));
                }
                else
                {
                    db.StringSet(MergeKey(key, region), value);
                }
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
            var endpoints = _multiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _multiplexer.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        private IDatabase GetDataBase()
        {
            if (!_multiplexer.IsConnected)
            {
                _multiplexer = ConnectionMultiplexer.Connect("localhost");
            }

            return _multiplexer.GetDatabase();
        }

        private string MergeKey(string key, string region)
        {
            return string.IsNullOrWhiteSpace(region) ? key : $"{key}_{region}";
        }
    }
}
