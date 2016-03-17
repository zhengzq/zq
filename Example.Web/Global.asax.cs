using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Example.Web.Core.Application.Navigations;
using Example.Web.Core.Application.Permissions;
using Example.Web.Core.Data;
using Example.Web.Core.Extensions;
using Zq.Configurations;
using Zq.Ioc;
using Zq.JsonNet;
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

            System.Data.Entity.Database.SetInitializer<EfDbContext>(new CreateDatabaseIfNotExists());

            Configuration.Instance
                .UseIoc()
                .UseJson()
                .UseLog4Net()
                .UseRedis();

            var permissionService = ObjectLocator.Resolve<IPermissionService>();
            var navService = ObjectLocator.Resolve<INavService>();
            var navIds = navService.GetNavigationRecords().Select(x => x.NavigationId).ToList();

            permissionService.InitNavigationPermissionWithFunctionPermissionFromAssembly(navIds, "Example.Web");

        }
    }
}
