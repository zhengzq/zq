using System;
using System.Web.Mvc;

namespace Example.Core.Web.Json
{
    public class JsonManager: ResultManager
    {
       
        public const string ContentType = "application/json";

        #region JsonResult

        /// <summary>
        /// 成功实体
        ///     信息显示操作成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static JsonResult Success()
        {
            return Success(null, "操作成功！");
        }
        /// <summary>
        /// 成功实体
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static JsonResult Success(string msg)
        {
            return Success(null, msg);
        }
        /// <summary>
        /// 成功实体
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static JsonResult Success(object data)
        {
            return Success(data, null);
        }
        /// <summary>
        /// 创建一个成功的json返回实体
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        /// <returns>JsonModel实体</returns>
        public static JsonResult Success(object data, string msg)
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
        public static JsonResult Error(int code, string msg)
        {
            return Error(code, msg, (string)null);
        }
        /// <summary>
        /// 创建一个失败的json实体
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static JsonResult Error(int code, Exception error)
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
        public static JsonResult Error(int code, string msg, string error)
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
        public static JsonResult Error(int code, string msg, string[] error)
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
        public static JsonResult Error(int code, string msg, Exception error)
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
        public static JsonResult Error(int code, string msg, Exception[] error)
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
    }
}