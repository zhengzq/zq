using System.Web.Mvc;
using Example.Web.Core.Web.Authentication;
using Example.Web.Core.Web.CustomAttributes;

namespace Example.Web.Core.Web
{
    [AdminException]
    [Authorize]
    public class AdminController : Controller
    {
        public CurrentUser CurrentUser => WorkContext.CurrentUser();
      
    }
}