using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Example.Core.Web.Extensions;

namespace Example.Core.ViewModel
{
    public class ManagerModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

        [DisplayName("角色")]
        [SelectListData(typeof(RoleSelectListCollector))]
        [UIHint("DropDownList")]
        public int RoleId { get; set; }

        [DisplayName("登入名")]
        public string LoginName { get; set; }

        [DisplayName("真实姓名")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [UIHint("Password")]
        public string Password { get; set; }

        [DisplayName("确认密码")]
        [UIHint("Password")]
        public string RepeatPassword { get; set; }

        [DisplayName("联系电话")]
        [UIHint("PhoneNumber")]
        public string Cellphone { get; set; }

        [DisplayName("Email")]
        [UIHint("EmailAddress")]
        public string Email { get; set; }

        [DisplayName("是否启用")]
        public bool IsEnable { get; set; }
    }
}