using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web.Mvc;
using Example.Core.Application.Permissions;
using Example.Core.Application.Roles;
using Example.Core.Query;
using Example.Core.Query.Dto;
using Example.Core.Query.Options;
using Example.Core.ViewModel;
using Example.Core.Web;
using Example.Core.Web.Extensions;
using Example.Core.Web.Json;
using Newtonsoft.Json.Linq;
using Zq;
using Zq.Paging;

namespace Example.Web.Areas.Admin.Controllers
{
    public class RoleController : AdminController
    {
        private readonly IRoleQueryService _roleQueryService;
        private readonly IRoleService _roleService;
        public RoleController(IRoleQueryService roleQueryService
            , IRoleService roleService)
        {
            _roleQueryService = roleQueryService;
            _roleService = roleService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List([ModelBinder(typeof(Binder<RoleOption>))]PageOption<RoleOption> search)
        {
            var data = _roleQueryService.Find(search.Index, search.Size, search.Option);
            return JsonManager.Raw(new
            {
                draw = Request.Form["search"], //Datatables发送的参数                
                recordsFiltered = data.TotalItemCount, //条件过滤后总条数
                recordsTotal = data.TotalItemCount, //表总条数
                data = data.Serialize(), //数据结果集
            });
        }
        [HttpGet]
        public ActionResult Edit(string roleId)
        {
            var data = _roleQueryService.FindById(roleId);
            var model = data.ToModel();
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            return View(new RoleModel());
        }
    }
}