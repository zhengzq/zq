using System;
using System.Web;
using System.Web.Mvc;
using Example.Core.Application.Navigations;
using Example.Core.Application.Permissions;
using Example.Core.Application.Roles;
using Example.Core.Web.Authentication;
using Example.Core.Web.Json;
using Zq.DI;

namespace Example.Core.Web.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionId PermissionId { get; private set; }

        public AdminAuthorizeAttribute(PermissionId permissionId)
        {
            this.PermissionId = permissionId;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var currentUser = WorkContext.CurrentUser;
            if (!currentUser.IsSys)
            {
                var roleService = Ioc.Resolve<IRoleService>();
                var navigationService = Ioc.Resolve<INavService>();
                var permissionService = Ioc.Resolve<IRoleService>();
                if (!roleService.CheckRoleIsAuthorized(PermissionId.ToString(), currentUser.RoleId))
                {
                    var user = WorkContext.CurrentUser ?? new CurrentUser();
                    var controller = filterContext.RouteData.Values["controller"].ToString();
                    var action = filterContext.RouteData.Values["action"].ToString();
                    var model = new UnAuthorizeModel(
                        message: $"您没有  的  操作权限",
                        controller: controller,
                        action: action,
                        username: user.UserName);

                    switch (filterContext.HttpContext.Request.HttpMethod.ToLower())
                    {
                        case "post":
                            filterContext.Result = JsonManager.Error(401, model.Message);
                            break;
                        default:
                            filterContext.Result = new ViewResult()
                            {
                                ViewName = "~/Views/Shared/UnAuthorize.cshtml",
                                ViewData = new ViewDataDictionary<UnAuthorizeModel>(model)
                            };
                            break;
                    }

                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
    }


}