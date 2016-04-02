﻿using System.Web.Mvc;
using Example.Core.Application.Managers;
using Example.Core.Domain.Managers;
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

        public AccountController(IAuthenticationService authenticationService, IManagerService managerService)
        {
            _authenticationService = authenticationService;
            _managerService = managerService;

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
                if (_managerService.ValidateLoginNameWithPassword(model.LoginName, model.Password))
                {
                    var r = _managerService.GetManagerByLoginName(model.LoginName);
                    if (r.State == ResultState.Success)
                    {
                        var manager = r.Data as Manager;
                        _authenticationService.SignIn(new CurrentUser()
                        {
                            IsSys = manager.IsSys,
                            UserName = manager.UserName,
                            LoginName = manager.LoginName,
                            //RoleName = manager.RoleName,
                            RoleName = "超级管理员",
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