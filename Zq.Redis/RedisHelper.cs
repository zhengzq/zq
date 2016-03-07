using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Caching;
using ServiceStack.Redis;

namespace Zq.Redis
{
    public class RedisHelper
    {
        static RedisHelper()
        {
            string[] endpoints = SentinelEndpoints.Split(',');
            SentinelHosts = endpoints.ToList();
            if (RedisSingleMode != null && RedisSingleMode.ToUpper() == RedisSingleModeOn)
            {
                RedisClient = new RedisClient(RedisHost, Convert.ToInt32(RedisPort));
            }
            else
            {
                redisSentinel = new RedisSentinel(SentinelHosts, MasterName);
            }
            prcm = CreateManager(new string[]
            {
                RedisHost + ":" + RedisPort
            }, new string[]
            {
                RedisHost + ":" + RedisPort
            });
        }

        private static string SentinelEndpoints = ConfigurationManager.AppSettings["SentinelEndpoints"];

        public static string RedisHost = ConfigurationManager.AppSettings["RedisHost"];
        public static string RedisPort = ConfigurationManager.AppSettings["RedisPort"];
        public static string RedisSingleMode = ConfigurationManager.AppSettings["RedisSingleMode"];

        public const string RedisSingleModeOn = "ON";
        public const string RedisSingleModeOff = "OFF";

        public static string MasterName = ConfigurationManager.AppSettings["MasterName"];

        public static List<string> SentinelHosts;

        public static IRedisSentinel redisSentinel;

        public static RedisClient RedisClient;

        public static PooledRedisClientManager prcm;

        private static IRedisClient GetClient()
        {
            var clientsManager = redisSentinel.Start();
            var rc = clientsManager.GetClient();

            return rc;
        }
        public static IRedisClient GetReadOnlyClient()
        {
            var clientsManager = redisSentinel.Start();
            var rc = clientsManager.GetReadOnlyClient();

            return rc;
        }
        private static ICacheClient GetReadOnlyCacheClient()
        {
            try
            {
                var clientsManager = redisSentinel.Start();
                var rc = clientsManager.GetReadOnlyCacheClient();

                return rc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static ICacheClient GetCacheClient()
        {
            var clientsManager = redisSentinel.Start();
            var rc = clientsManager.GetCacheClient();

            return rc;
        }
        public static ICacheClient ReadClinet()
        {
            if (RedisSingleMode != null && RedisSingleMode.ToUpper() == RedisSingleModeOn)
            {
                var client = prcm.GetCacheClient();

                return client;

                //using (var manager = new RedisManagerPool(RedisHost))
                //{
                //    return manager.GetClient();
                //}
                //return RedisClient;
            }

            var clinet = GetReadOnlyCacheClient();
            return clinet ?? GetReadOnlyClient();
        }
        public static ICacheClient WriteClinet()
        {
            //默认使用redis 哨兵模式
            if (RedisSingleMode != null && RedisSingleMode.ToUpper() == RedisSingleModeOn)
            {
                var client = prcm.GetCacheClient();

                return client;

                //using (var manager = new RedisManagerPool(RedisHost))
                //{
                //    return manager.GetClient();
                //}
                //return RedisClient;
            }

            var clinet = GetCacheClient();
            return clinet ?? GetClient();
        }

        public static PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            //支持读写分离，均衡负载
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 5,//“写”链接池链接数
                MaxReadPoolSize = 5,//“写”链接池链接数
                AutoStart = true,
            });
        }
     
    }
}
