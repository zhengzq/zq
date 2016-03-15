using System.Web.Mvc;
using Example.Web.Core.Web.Json;
using Zq.Ioc;
using Zq.Logging;

namespace Example.Web.Core.Web.CustomAttributes
{
    public class AdminExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            ObjectLocator.Resolve<ILogger>().Log(LogLevel.Error, filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.Result = JsonManager.GetError(500, filterContext.Exception.ToString());

            base.OnException(filterContext);
        }
    }
}