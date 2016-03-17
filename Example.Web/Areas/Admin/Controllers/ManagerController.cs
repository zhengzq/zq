using System.Web.Mvc;
using Example.Web.Core.Query;
using Example.Web.Core.Web;
using System.Linq;
using Example.Web.Core.Application.Permissions;
using Example.Web.Core.Web.CustomAttributes;
using Example.Web.Core.Web.Json;

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
                data = data.Select(x => new
                {
                    Id = x.Id,
                    LoginName = x.LoginName,
                    UserName = x.UserName,
                    CellPhone = x.Cellphone,
                    RoleName = x.RoleName,
                }).ToList(), //数据结果集
            });
        }

    }
}