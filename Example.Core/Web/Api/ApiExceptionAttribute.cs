using System;
using System.Net.Http;
using System.Web.Http.Filters;
using Zq.DI;
using Zq.Logging;
using Zq.Serializers;

namespace Example.Core.Web.Api
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //这里可以加入Log部分，记录异常日志
            var converter = Ioc.Resolve<IJsonSerializer>();
          
            string logMsg = string.Format("Message:{0}\r<br/>\r ActionArguments:{1}\r<br/>\r Url:{2}", 
                actionExecutedContext.Exception.Message
                , converter.Serialize(actionExecutedContext.ActionContext.ActionArguments)
                , actionExecutedContext.Request.RequestUri);
            var logger = Ioc.Resolve<ILogger>();
            logger.Log(LogLevel.Error, logMsg, actionExecutedContext.Exception);

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(new
            {
                State = false,
                Msg = actionExecutedContext.Exception.Message
            });
            base.OnException(actionExecutedContext);
        }
    }

}
