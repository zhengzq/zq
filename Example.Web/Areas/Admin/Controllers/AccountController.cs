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
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_managerService.ValidateLoginNameWithPassword(model.LoginName, model.Password))
                {
                    var r = _managerService.GetManagerByLoginName(model.LoginName);
                    if (r.State == ResultState.Success)
                    {
                        var manager = r.Data as Manager;
                        _authenticationService.SignIn(new CurrentUser()
                        {
                            IsSys = manager.IsSys,
                            //RealName = manager.RealName,
                            //RoleName = manager.RoleName,
                            RoleId = manager.RoleId,
                            UserId = manager.Id
                        }, model.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        model.Error = r.Msg;
                    }
                }
                else
                {
                    model.Error = "账号或密码错误";
                }
            }
            else
            {
                model.Error = ModelState.SerializeErrorToString();
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}