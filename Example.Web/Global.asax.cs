using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Zq.Autofac;
using Zq.Configurations;
using Zq.Log4net;

namespace Example.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Configuration.Instance
                .UseAutofac()
                .UseLog4Net();
        }
    }
}
