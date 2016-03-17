using System.Web.Mvc;

namespace Example.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/admin/home/index");
        }

      
    }
}