using System.Web.Mvc;

namespace Example2.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/admin/home/index");
        }
    }
}