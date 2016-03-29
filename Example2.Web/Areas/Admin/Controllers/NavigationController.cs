using System.Linq;
using System.Web.Mvc;
using Example.Core.Query;
using Example.Core.Web;

namespace Example2.Web.Areas.Admin.Controllers
{
    public class NavigationController : AdminController
    {
        private readonly IRoleQueryService _roleQueryService;
        private readonly INavQueryService _navQueryService;

        public NavigationController(IRoleQueryService roleQueryService
            , INavQueryService navQueryService)
        {
            _roleQueryService = roleQueryService;
            _navQueryService = navQueryService;
        }
        public JsonResult Tree()
        {
            var list = _navQueryService.GetModules().Select(x =>
             {
                 return new
                 {
                     text = x.Title,
                     children = x.Childs.Select(y => new
                     {
                         iconCls = y.Icon,
                         text = y.Title,
                         href = y.Url
                     })
                 };
             }).ToList();
            return Json(list);
        }
    }
}