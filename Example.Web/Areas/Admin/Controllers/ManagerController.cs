using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Example.Core.Application.Managers;
using Example.Core.Application.Permissions;
using Example.Core.Query;
using Example.Core.Query.Dto;
using Example.Core.Query.Options;
using Example.Core.ViewModel;
using Example.Core.Web;
using Example.Core.Web.CustomAttributes;
using Example.Core.Web.Json;
using Zq;
using Zq.Paging;

namespace Example.Web.Areas.Admin.Controllers
{
    public class ManagerController : AdminController
    {
        private readonly IManagerService _managerService;
        private readonly IManagerQueryService _managerQueryService;

        public ManagerController(IManagerQueryService managerQueryService
            , IManagerService managerService)
        {
            _managerQueryService = managerQueryService;
            _managerService = managerService;
        }

        [AdminAuthorize(PermissionId.ManagerView)]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AdminAuthorize(PermissionId.ManagerView)]
        [HttpPost]
        public JsonResult List([ModelBinder(typeof(Binder<ManagerOption>))]PageOption<ManagerOption> search)
        {
            var data = _managerQueryService.Find(search.Index, search.Size, search.Option);
            return JsonManager.Raw(new
            {
                draw = Request.Form["search"], //Datatables发送的参数                
                recordsFiltered = data.TotalItemCount, //条件过滤后总条数
                recordsTotal = data.TotalItemCount, //表总条数
                data = data.Serialize(), //数据结果集
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _managerQueryService.FindById(id).ToModel();
            return View(model);
        }
        [HttpPost]
        public JsonResult Edit(ManagerModel model)
        {
            var r = _managerService.Update(new UpdateManagerCommand(model.Id
                  , model.UserName
                  , model.Password
                  , model.RoleId
                  , model.Cellphone
                  , model.Email
                  , model.IsEnable));
            return r.State == ResultState.Success
             ? JsonManager.Success("修改成功")
             : JsonManager.Error(r.Code, r.Msg);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View(new ManagerModel());
        }
        [HttpPost]
        public JsonResult Add(ManagerModel model)
        {
            var r = _managerService.Create(new CreateManagerCommand(model.LoginName
                , model.UserName
                , model.Password
                , model.RepeatPassword
                , model.RoleId
                , model.Cellphone
                , model.Email
                , model.IsEnable));
            return r.State == ResultState.Success
             ? JsonManager.Success("添加成功")
             : JsonManager.Error(r.Code, r.Msg);
        }
    }
}