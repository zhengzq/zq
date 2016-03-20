using System.Web.Mvc;
using Example.Web.Core.Query;
using Example.Web.Core.Web;
using System.Linq;

namespace Example.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IRoleQueryService _roleQueryService;
        private readonly INavQueryService _navQueryService;

        public HomeController(IRoleQueryService roleQueryService
            , INavQueryService navQueryService)
        {
            _roleQueryService = roleQueryService;
            _navQueryService = navQueryService;
        }

        public ActionResult Index()
        {
            //var list = CurrentUser.IsSys
            //    ? _navQueryService.GetModules()
            //    : _roleQueryService.GetModulesByRoleId(CurrentUser.RoleId);

            ViewData["Navigations"] = _navQueryService.GetModules();
            

            return View();
        }
        public ActionResult Main()
        {
            return View();
        }

        
    }
}