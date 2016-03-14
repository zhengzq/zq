namespace Example.Web.Areas.Admin.Models
{
    public class LoginModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}