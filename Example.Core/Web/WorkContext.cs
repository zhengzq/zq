using System.Configuration;
using Example.Core.Web.Authentication;
using Zq.Ioc;

namespace Example.Core.Web
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