using System.Web.Mvc;
using Example.Web.Areas.Admin.Models;
using Example.Web.Core.Application.Managers;
using Example.Web.Core.Domain.Managers;
using Example.Web.Core.Extensions;
using Example.Web.Core.Web.Authentication;
using Example.Web.Core.Web.Json;
using Zq;

namespace Example.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IManagerService _managerService;

        public AccountController(IAuthenticationService authenticationService, IManagerService managerService)
        {
            _authenticationService = authenticationService;
            _managerService = managerService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var b = _managerService.ValidateLoginNameWithPassword(model.LoginName, model.Password);
                if (!b) return JsonManager.Error(200, "账号或密码错误");

               
                var r = _managerService.GetManagerByLoginName(model.LoginName);
                if (r.State != ResultState.Success) return JsonManager.Error(300, r.Msg);
                var manager = r.Data as Manager;
                _authenticationService.SignIn(new CurrentUser()
                {
                    IsSys = manager.IsSys,
                    //RealName = manager.RealName,
                    //RoleName = manager.RoleName,
                    RoleId = manager.RoleId,
                    UserId = manager.Id
                }, model.RememberMe);

                return JsonManager.Success();
            }
            return JsonManager.Error(200, ModelState.SerializeErrorToString());
        }

        public ActionResult LogOut()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}