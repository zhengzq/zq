using System.Linq;
using System.Security;
using System.Web.Mvc;
using System.Web.Routing;
using Example.Web.Core.Application.Navigations;
using Example.Web.Core.Application.Permissions;
using Example.Web.Core.Extensions;
using Zq.Configurations;
using Zq.Ioc;
using Zq.Log4net;
using Zq.Redis;

namespace Example.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Configuration.Instance
                .UseIoc()
                .UseLog4Net()
                .UseRedis();

            var permissionService = ObjectLocator.Resolve<IPermissionService>();
            var navService = ObjectLocator.Resolve<INavService>();
            var navCodes = navService.GetNavigationRecords().Select(x => x.NavigationId).ToList();

            permissionService.InitFunctionPermissionFromAssembly("Example.Web");
            permissionService.InitNavigationPermission(navCodes);
        }
    }
}
