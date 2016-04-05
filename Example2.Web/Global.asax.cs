using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Example.Core.Application.Navigations;
using Example.Core.Application.Permissions;
using Example.Core.Data;
using Zq.Configurations;
using Zq.DI;
using Zq.JsonNet;
using Zq.Log4net;
using Zq.Logging;
using Zq.Redis;

namespace Example2.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                System.Data.Entity.Database.SetInitializer<EfDbContext>(new CreateDatabaseIfNotExists());

                Configuration.Instance
                    .UseIoc()
                    .UseJson()
                    .UseLog4Net()
                    .UseRedis();

                var permissionService = Ioc.Resolve<IPermissionService>();
                var navService = Ioc.Resolve<INavService>();
                var navIds = navService.GetNavigationRecords().Select(x => x.NavigationId).ToList();

                permissionService.InitNavigationPermissionWithFunctionPermissionFromAssembly(navIds, "Example2.Web");
            }
            catch (System.Exception ex)
            {
                Ioc.Resolve<ILogger>().Log(LogLevel.Error, ex);
                throw;
            }

        }
    }
}
