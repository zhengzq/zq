using System.Configuration;
using System.Web;
using Example.Web.Core.Web.Authentication;
using Zq.Ioc;

namespace Example.Web.Core.Web
{
    public static class WorkContext
    {
      
        public static CurrentUser CurrentUser {
            get
            {
                return ObjectLocator.Resolve<IAuthenticationService>().GetAuthenticatedUser();
            }
        }

        public static string JsVersion
        {
            get { return ConfigurationManager.AppSettings["JsVersion"]; }
        }

        public static string CssVersion
        {
            get { return ConfigurationManager.AppSettings["CssVersion"]; }
        }

    }
}