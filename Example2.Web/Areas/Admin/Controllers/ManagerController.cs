using System;
using System.Web.Mvc;
using Example.Core.Query;
using Example.Core.Web;
using Example.Core.Web.Json;

namespace Example2.Web.Areas.Admin.Controllers
{
    public class ManagerController : AdminController
    {

        private readonly IManagerQueryService _managerQueryService;

        public ManagerController(IManagerQueryService managerQueryService)
        {
            _managerQueryService = managerQueryService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult List(int page, int rows, string query)
        {
            var data = _managerQueryService.Find(page, rows, null);

            return Json(new { total = data.TotalItemCount, rows = data });
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

    }
}
