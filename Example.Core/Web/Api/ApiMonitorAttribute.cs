using System;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Zq.DI;
using Zq.Logging;

namespace Example.Core.Web.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ApiMonitorAttribute : ActionFilterAttribute
    {
        public string UserName { get; set; }
        public Stopwatch Watch { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Watch = new Stopwatch();
            Watch.Start();
            UserName = HttpContext.Current.User.Identity.Name;
            base.OnActionExecuting(actionContext);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Controller:{0}  ");
            sb.AppendLine("Action：{1}  ");
            sb.AppendLine("执行间隔:{2}  ");
            sb.AppendLine("操作人:{3}  ");
            var values = actionExecutedContext.ActionContext.ControllerContext.RouteData.Values;
            var controller = values["controller"];
            var action = values["action"];
            var logger = Ioc.Resolve<ILogger>(new Tuple<string, object>("name", "Monitor"));
            logger.Log(LogLevel.Info, sb.ToString()
                , controller
                , action
                , Watch.ElapsedMilliseconds
                , UserName);
            Watch.Stop();

            base.OnActionExecuted(actionExecutedContext);
        }

    }
    public class MonitorModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime ExecuteStartTime { get; set; }
        public DateTime ExecuteEndTime { get; set; }
    }
}
