using System.Web.Mvc;
using Example.Core.Web;

namespace Example.Web.Areas.Admin.Controllers
{
    public class ToolController : AdminController
    {
       
        public ActionResult FormBuild()
        {
            return View();
        }
    }
}