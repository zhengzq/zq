//using System;
//using StackExchange.Redis;
//using Zq.Caching;
//using Zq.Ioc;
//using Zq.Logging;
//using Zq.Serializers;

//namespace Zq.Redis
//{
//    public partial class RedisCacheManager : ICacheManager
//    {
//        private ConnectionMultiplexer _redis;
//        private int _db;
//        public RedisCacheManager(string redisConnString, int dbNum)
//        {
//            _redis = ConnectionMultiplexer.Connect(redisConnString);
//            _db = dbNum;
//        }

//        public T Get<T>(string key, string region = "")
//        {
//            try
//            {
//                if (!string.IsNullOrWhiteSpace(region))
//                    key = $"{region}_{key}";

//                var db = _redis.GetDatabase(_db);
//                return ObjectLocator.Resolve<IJsonSerializer>().Deserialize<T>(db.StringGet(key).ToString());
//            }
//            catch (Exception ex)
//            {
//                ObjectLocator.Resolve<ILogger>().Error(ex);
//                return Activator.CreateInstance<T>();
//            }
//        }

//        public void Set(string key, object data, int cacheTime, string region = "")
//        {
//            try
//            {
//                if (!string.IsNullOrWhiteSpace(region))
//                    key = string.Format("{0}_{1}", region, key);


//            }
//            catch (Exception ex)
//            {
//                ObjectLocator.Resolve<ILogger>().Error(ex);
//            }
//        }

//        public bool IsSet(string key, string region = "")
//        {
//            if (!string.IsNullOrWhiteSpace(region))
//                key = string.Format("{0}_{1}", region, key);

//            return base.HashSet()
//        }

//        public void Remove(string key, string region = "")
//        {
//            if (!string.IsNullOrWhiteSpace(region))
//                key = $"{region}_{key}";

//            base.DelKey(key);
//        }

//        public void RemoveByPattern(string pattern)
//        {

//        }

//        public void Clear()
//        {
//            RedisHelper.WriteClinet().FlushAll();
//        }
//    }
//}
