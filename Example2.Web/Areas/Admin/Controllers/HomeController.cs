using System.Web.Mvc;
using Example.Core.Web;

namespace Example2.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
