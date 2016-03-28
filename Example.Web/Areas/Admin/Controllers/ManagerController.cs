using System.Web.Mvc;
using System.Linq;
using Example.Core.Application.Permissions;
using Example.Core.Query;
using Example.Core.Web;
using Example.Core.Web.CustomAttributes;

namespace Example.Web.Areas.Admin.Controllers
{
    public class ManagerController : AdminController
    {

        private readonly IManagerQueryService _managerQueryService;

        public ManagerController(IManagerQueryService managerQueryService)
        {
            _managerQueryService = managerQueryService;
        }
       
        [AdminAuthorize(PermissionId.ManagerView)]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AdminAuthorize(PermissionId.ManagerView)]
        [HttpPost]
        public JsonResult List([ModelBinder(typeof(Binder<ManagerOption>))]Page<ManagerOption> search)
        {
            var data = _managerQueryService.Find(search.Index, search.Size, search.Option);
            return Json(new
            {
                draw = Request.Form["search"], //Datatables发送的参数                
                recordsFiltered = data.TotalItemCount, //条件过滤后总条数
                recordsTotal = data.TotalItemCount, //表总条数
                data = data, //数据结果集
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}