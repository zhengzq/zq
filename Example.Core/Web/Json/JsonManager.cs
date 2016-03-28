using System;
using System.Web.Mvc;

namespace Example.Core.Web.Json
{
    /// <summary>
    /// json通信实体
    /// </summary>
    [Serializable]
    public class JsonModel
    {
        /// <summary>
        /// 错误码(0表示无错误)
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string[] Error { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// Json数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 服务器响应时间 
        /// </summary>
        public long ResponseTicks
        {
            get
            {
                return DateTime.Now.Ticks;
            }
        }

    }
    public class JsonManager
    {
        public static JsonErrorType JsonErrorType = JsonErrorType.Message;
        public const string ContentType = "application/json";

        #region JsonModel
        public static dynamic Success()
        {
            return Success(null, "操作成功！");
        }
        /// <summary>
        /// 成功实体
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static dynamic Success(string msg)
        {
            return Success(null, msg);
        }
        /// <summary>
        /// 成功实体
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static dynamic Success(object data)
        {
            return Success(data, null);
        }
        /// <summary>
        /// 创建一个成功的json返回实体
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        /// <returns>JsonModel实体</returns>
        public static dynamic Success(object data, string msg)
        {
            return new 
            {
                Code = 0,
                Data = data,
                State = true,
                Msg = msg
            };
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public static dynamic Error(int code, string msg)
        {
            return Error(code, msg, (string)null);
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static dynamic Error(int code, Exception error)
        {
            return Error(code, error == null ? null : error.Message, error);
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="error">错误信息</param>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public static dynamic Error(int code, string msg, string error)
        {
            return Error(code, msg, error == null ? null : new string[1] { error });
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="error">错误信息</param>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public static dynamic Error(int code, string msg, string[] error)
        {
            return new
            {
                Code = code,
                Error = error,
                State = false,
                Msg = msg
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static dynamic Error(int code, string msg, Exception error)
        {
            return Error(code, msg, new string[1] { GetException(error) });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static dynamic Error(int code, string msg, Exception[] error)
        {
            return new
            {
                Code = code,
                Error = GetException(error),
                State = false,
                Msg = msg
            };
        }

        #endregion

        #region JsonResult

        /// <summary>
        /// 成功实体
        ///     信息显示操作成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static JsonResult GetSuccess()
        {
            return GetSuccess(null, "操作成功！");
        }
        /// <summary>
        /// 成功实体
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static JsonResult GetSuccess(string msg)
        {
            return GetSuccess(null, msg);
        }
        /// <summary>
        /// 成功实体
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static JsonResult GetSuccess(object data)
        {
            return GetSuccess(data, null);
        }
        /// <summary>
        /// 创建一个成功的json返回实体
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        /// <returns>JsonModel实体</returns>
        public static JsonResult GetSuccess(object data, string msg)
        {
            return Get(new JsonModel()
            {
                Code = 0,
                Data = data,
                Error = null,
                State = true,
                Msg = msg
            });
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public static JsonResult GetError(int code, string msg)
        {
            return GetError(code, msg, (string)null);
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static JsonResult GetError(int code, Exception error)
        {
            return GetError(code, error == null ? null : error.Message, error);
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="error">错误信息</param>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public static JsonResult GetError(int code, string msg, string error)
        {
            return GetError(code, msg, error == null ? null : new string[1] { error });
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="error">错误信息</param>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public static JsonResult GetError(int code, string msg, string[] error)
        {
            return Get(new JsonModel()
            {
                Code = code,
                Data = null,
                Error = error,
                State = false,
                Msg = msg
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static JsonResult GetError(int code, string msg, Exception error)
        {
            return GetError(code, msg, new string[1] { GetException(error) });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static JsonResult GetError(int code, string msg, Exception[] error)
        {
            return Get(new JsonModel()
            {
                Code = code,
                Data = null,
                Error = GetException(error),
                State = false,
                Msg = msg
            });
        }
        //
        public static JsonResult Get(JsonModel jsonModel)
        {
            if (jsonModel == null) throw new ArgumentNullException("jsonModel");
            return new JsonNetResult()
            {
                Data = jsonModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 显示原始传入的数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult Raw(object data)
        {
            return new JsonResult() { Data = data };
        }
        #endregion

        #region private method
        private static string GetException(Exception error)
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
        private static string[] GetException(Exception[] errors)
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
     
    }
    public enum JsonErrorType
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        Message,
        /// <summary>
        /// 堆栈
        /// </summary>
        Stack,
        /// <summary>
        /// 详细
        /// </summary>
        Detail
    }
}