﻿//using System;
//using System.Collections.Generic;
//using System.IO;
//using StackExchange.Redis;

//namespace Zq.Redis
//{
//    public abstract class RedisBase : IDisposable
//    {
//        private ConnectionMultiplexer redis;
//        private int db;
//        public RedisBase(string redisConnString, int dbNum)
//        {
//            redis = ConnectionMultiplexer.Connect(redisConnString);
//            db = dbNum;
//        }

//        /// <summary>
//        /// 统一管理redis命名规则
//        /// </summary>
//        private static Dictionary<string, string> RedisKeyConfig = new Dictionary<string, string>(){
//            {"HashKey1","id:{0}"},
//            {"HashKey2","id:{0}"},
//            {"HashKey3","id:{0}"}
//        };

//        private static Dictionary<string, string> RdisSetKey = new Dictionary<string, string>()
//        {
//            {"SetKey1","curcarset"},
//            {"SetKey2","curcsset"}
//        };

//        /// <summary>
//        /// 根据key获取field
//        /// </summary>
//        /// <param name="key">key</param>
//        /// <param name="fieldId">filed的标识</param>
//        /// <returns></returns>
//        private string GetField(string key, string fieldId)
//        {
//            if (RedisKeyConfig.ContainsKey(key))
//            {
//                string fieldFormat = RedisKeyConfig[key];

//                return string.Format(fieldFormat, fieldId);

//            }
//            else
//            {

//                throw new Exception("redis 不存这个键");
//            }

//        }

//        /// <summary>
//        /// 根据key获取set的名字
//        /// </summary>
//        /// <param name="key">key</param>
//        /// <returns></returns>
//        private string GetSetKey(string key)
//        {
//            if (RdisSetKey.ContainsKey(key))
//            {
//                return RdisSetKey[key];
//            }
//            else
//            {
//                throw new Exception("redis 不存这个键");
//            }

//        }
//        /// <summary>
//        ///  批量删除
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="delIds"></param>
//        /// <returns></returns>
//        protected int HashDelete(string key, List<int> delIds)
//        {
//            if (delIds == null || delIds.Count <= 0)
//                return 1;

//            var dataBase = redis.GetDatabase(db);

//            int count = delIds.Count;

//            RedisValue[] delValues = new RedisValue[count];

//            for (int i = 0; i < count; i++)
//            {
//                delValues[i] = GetField(key, delIds[i].ToString());
//            }

//            return dataBase.HashDelete(key, delValues) >= 0 ? 1 : 0;
//        }

//        /// <summary>
//        /// 比较删除
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="comIds"></param>
//        /// <returns></returns>
//        protected int HashCompareDel(string key, List<int> comIds)
//        {

//            if (comIds == null || comIds.Count <= 0)
//                return 1;

//            var dataBase = redis.GetDatabase(db);

//            string curSetKey = GetSetKey(key);

//            //  如果不存在这个set
//            if (!dataBase.KeyExists(curSetKey))
//            {
//                //  直接设置为当前set
//                return SetAdd(curSetKey, comIds);

//            }
//            else   //  存在时比较删除
//            {
//                string comSetKey = curSetKey + "_new";

//                SetAdd(comSetKey, comIds);

//                // 取出原来set里有，但是新set里没有的值
//                RedisValue[] diffValue = dataBase.SetCombine(SetOperation.Difference, curSetKey, comSetKey);

//                if (diffValue != null && diffValue.Length > 0)
//                {
//                    //  转化为hash的filed
//                    for (int i = 0; i < diffValue.Length; i++)
//                    {
//                        diffValue[i] = GetField(key, diffValue[i]);
//                    }

//                    //  删除
//                    if (dataBase.HashDelete(key, diffValue) > 0)
//                    {
//                        dataBase.KeyRename(comSetKey, curSetKey);
//                        return 1;
//                    }
//                    else
//                    {
//                        return 0;
//                    }
//                }
//                else  //  如果前后结果一致，没有需要删除的，删除比较Set
//                {
//                    dataBase.KeyDelete(comSetKey);
//                    return 1;
//                }
//            }


//        }

//        /// <summary>
//        /// 设置集合
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="listIds"></param>
//        /// <returns></returns>
//        private int SetAdd(string key, List<int> listIds)
//        {

//            var dataBase = redis.GetDatabase(db);

//            dataBase.StringSet(key, "ab");

//            dataBase.StringGet(key);

//            int count = listIds.Count;

//            RedisValue[] setValues = new RedisValue[count];

//            for (int i = 0; i < count; i++)
//            {
//                setValues[i] = listIds[i];
//            }

//            return dataBase.SetAdd(key, setValues) > 0 ? 1 : 0;

//        }

//        /// <summary>
//        /// 更新数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="listInstance"></param>
//        /// <param name="getModelId"></param>
//        /// <returns></returns>
//        protected int HashSet<T>(string key, List<T> listInstance, Func<T, string> getModelId)
//        {
//            if (listInstance == null || listInstance.Count <= 0)
//                return 1;

//            var dataBase = redis.GetDatabase(db);

//            int count = listInstance.Count;

//            HashEntry[] upValues = new HashEntry[count];

//            for (int i = 0; i < count; i++)
//            {
//                var item = listInstance[i];

//                using (MemoryStream ms = new MemoryStream())
//                {
//                    ProtoBuf.Serializer.Serialize<T>(ms, item);

//                    upValues[i] = new HashEntry(GetField(key, getModelId(item)), ms.ToArray());
//                }
//            }

//            dataBase.HashSet(key, upValues);

//            return 1;

//        }

//        /// <summary>
//        /// 分批更新
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="listInstance"></param>
//        /// <param name="getModelId"></param>
//        /// <param name="batchCount"></param>
//        /// <returns></returns>
//        protected int BatchHashSet<T>(string key, List<T> listInstance, Func<T, string> getModelId, int batchCount)
//        {
//            if (listInstance == null || listInstance.Count <= 0)
//                return 1;

//            int count = listInstance.Count;

//            if (count <= batchCount)
//                return this.HashSet<T>(key, listInstance, getModelId);

//            var dataBase = redis.GetDatabase(db);

//            int batch = count / batchCount;

//            batch = count % batchCount == 0 ? batch : batch + 1;

//            for (int i = 0; i < batch; i++)
//            {
//                int thisCount = (i == batch - 1) ? count % batchCount : batchCount;
//                HashEntry[] upValues = new HashEntry[thisCount];

//                for (int j = 0; j < thisCount; j++)
//                {
//                    var item = listInstance[i * batchCount + j];

//                    using (MemoryStream ms = new MemoryStream())
//                    {
//                        ProtoBuf.Serializer.Serialize<T>(ms, item);

//                        upValues[j] = new HashEntry(GetField(key, getModelId(item)), ms.ToArray());
//                    }
//                }

//                dataBase.HashSet(key, upValues);

//            }

//            return 1;

//        }

//        /// <summary>
//        /// 获取列表
//        /// </summary>
//        /// <typeparam name="T">获取什么类型的列表</typeparam>
//        /// <param name="key">Redis Key</param>
//        /// <param name="listFieldId">标识ID</param>
//        /// <returns></returns>
//        protected List<T> HashGet<T>(string key, List<int> listFieldId)
//        {
//            List<T> listResult = new List<T>();

//            if (listFieldId == null || listFieldId.Count < 0)
//            {
//                return listResult;
//            }

//            var dataBase = redis.GetDatabase(db);

//            int count = listFieldId.Count;

//            //  转化为Redis数据结构
//            RedisValue[] filedValue = new RedisValue[count];
//            for (int i = 0; i < count; i++)
//            {
//                filedValue[i] = GetField(key, listFieldId[i].ToString());
//            }

//            // 取出结果集
//            RedisValue[] arrResult = dataBase.HashGet(key, filedValue);

//            // 将Redis结果转为化所需类型的列表
//            if (arrResult != null && arrResult.Length > 0)
//            {
//                foreach (RedisValue item in arrResult)
//                {
//                    if (!item.IsNullOrEmpty)
//                    {
//                        using (MemoryStream ms = new MemoryStream((byte[])item))
//                        {
//                            listResult.Add(ProtoBuf.Serializer.Deserialize<T>(ms));
//                        }
//                    }
//                }
//            }
//            return listResult;
//        }

//        /// <summary>
//        /// 获取单个结果
//        /// </summary>
//        /// <typeparam name="T">获取结果的类型</typeparam>
//        /// <param name="key">Redis Key</param>
//        /// <param name="fieldId">Redis Field 标识</param>
//        /// <returns></returns>
//        protected T HashGet<T>(string key, int fieldId)
//        {
//            if (fieldId > 0)
//            {
//                var dataBase = redis.GetDatabase(db);

//                // 取出结果
//                RedisValue result = dataBase.HashGet(key, GetField(key, fieldId.ToString()));

//                // 将Redis结果转为化所需类型的列表
//                if (!result.IsNullOrEmpty)
//                {
//                    using (MemoryStream ms = new MemoryStream((byte[])result))
//                    {
//                        return ProtoBuf.Serializer.Deserialize<T>(ms);
//                    }
//                }
//            }
//            return default(T);
//        }

//        protected int DelKey(string key)
//        {
//            if (key == null) throw new ArgumentNullException(nameof(key));
//            if (RedisKeyConfig.ContainsKey(key))
//            {
//                var dataBase = redis.GetDatabase(db);

//                dataBase.KeyDelete(key);

//            }
//            return 1;
//        }


//        public void Dispose()
//        {
//            if (redis != null)
//                redis.Dispose();
//        }
//    }
//}