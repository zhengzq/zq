using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Example2.Web.Areas.Admin.Models
{
    public class LoginModel
    {
        [DisplayName("登入账号")]
        [Required(ErrorMessage = "请输入账号")]
        public string LoginName { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Error { get; set; }
    }
}