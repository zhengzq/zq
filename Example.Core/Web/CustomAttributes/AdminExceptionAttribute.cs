using System;
using System.Web;
using System.Web.Mvc;
using Example.Core.Web.Json;
using Zq.DI;
using Zq.Logging;

namespace Example.Core.Web.CustomAttributes
{
    public class AdminExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.IsChildAction)
            {
                return;
            }

            //当Config 配置为On时，才会保存Error日志
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var exception = filterContext.Exception;
            var httpCode = new HttpException(null, exception).GetHttpCode();

            if (httpCode == 404)
            {
                filterContext.Result = new RedirectResult("/Areas/Admin/404.html");
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 404;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                return;
            }

            if (httpCode != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(exception))
            {
                return;
            }

            var loginName = string.Empty;
            if (WorkContext.CurrentUser != null)
                loginName = WorkContext.CurrentUser.LoginName;

            var errMsg = string.Format("【{0}】当前用户：{1}  异常信息：{2} ", DateTime.Now, loginName, exception);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = JsonManager.GetError(500, "服务器正忙,请稍候再试。")
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Areas/Admin/Views/Shared/Error.cshtml",
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            Ioc.Resolve<ILogger>().Log(LogLevel.Error, errMsg);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

    }
}