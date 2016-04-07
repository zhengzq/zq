using System.Web.Mvc;
using Example.Core.Application.Managers;
using Example.Core.Domain.Managers;
using Example.Core.Query;
using Example.Core.ViewModel;
using Example.Core.Web;
using Example.Core.Web.Authentication;
using Example.Core.Web.Extensions;
using Zq;

namespace Example.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IManagerService _managerService;
        private readonly IManagerQueryService _managerQueryService;

        public AccountController(IAuthenticationService authenticationService
            , IManagerService managerService
            , IManagerQueryService managerQueryService)
        {
            _authenticationService = authenticationService;
            _managerService = managerService;
            _managerQueryService = managerQueryService;
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var r = _managerService.ValidateLoginNameWithPassword(model.LoginName, model.Password);
                if (r.State == ResultState.Success)
                {
                    var dto = _managerQueryService.FindByLoginName(model.LoginName);
                    if (dto != null)
                    {
                        _authenticationService.SignIn(new CurrentUser()
                        {
                            IsSys = dto.IsSys,
                            UserName = dto.UserName,
                            LoginName = dto.LoginName,
                            RoleName = dto.RoleName,
                            RoleId = dto.RoleId,
                            UserId = dto.Id
                        }, model.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        model.Error = "账号获取异常";
                    }
                }
                else
                {
                    model.Error = r.Msg;
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