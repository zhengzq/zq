using System;

namespace Example.Core.Web.Json
{
    public class DynamicManager : ResultManager
    {
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


    }

}
