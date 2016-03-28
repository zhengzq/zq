using System.Web.Mvc;
using Example.Core.Web.Authentication;
using Example.Core.Web.CustomAttributes;

namespace Example.Core.Web
{
    [AdminException]
    [Authorize]
    public class AdminController : Controller
    {
        public CurrentUser CurrentUser => WorkContext.CurrentUser;
        //public CurrentUser CurrentUser
        //{
        //    get
        //    {
        //        return new CurrentUser()
        //        {
        //            IsSys=true,
        //            RealName = "ZZQ",
        //            RoleId = 1,
        //            RoleName = "超级管理员",
        //            UserId = 1
        //        };
        //    }
        //}
    }
}