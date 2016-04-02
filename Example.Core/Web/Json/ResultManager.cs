using System;

namespace Example.Core.Web.Json
{
    public abstract class ResultManager
    {
        public static JsonErrorType JsonErrorType = JsonErrorType.Message;

        #region public method
        /// <summary>
        /// .net数据类型序列化对象到json字符串
        /// </summary>
        /// <param name="objectValue"></param>
        /// <returns></returns>
        public static string Serialize(object objectValue)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(objectValue);
        }
        /// <summary>
        /// 反序列化json字符串到.net数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }
        /// <summary>
        /// 反序列化json字符串到.net数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static object Deserialize(string jsonString)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
        }
        public static object Deserialize(string jsonString, Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, type);
        }
        #endregion

        #region private method
        protected static string GetException(Exception error)
        {
            if (error == null) return null;
            switch (JsonErrorType)
            {
                case JsonErrorType.Message:
                    return error.Message;
                case JsonErrorType.Stack:
                    return error.StackTrace;
                case JsonErrorType.Detail:
                default:
                    return error.ToString();
            }
        }
        protected static string[] GetException(Exception[] errors)
        {
            if (errors == null) return null;
            string[] es = null;

            es = new string[errors.Length];
            for (var i = 0; i < errors.Length; i++)
            {
                es[i] = GetException(errors[i]);
            }
            return es;
        }
        #endregion
    }

}
