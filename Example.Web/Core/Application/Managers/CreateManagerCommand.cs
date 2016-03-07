namespace Example.Web.Core.Application.Managers
{
    public class CreateManagerCommand
    {
        public CreateManagerCommand( string loginName
            , string userName
            , string password
            , string repeatPassword
            , int roleId
            , string cellphone
            , string email
            , bool isEnable)
        {
            this.LoginName = loginName;
            this.UserName = userName;
            this.Password = password;
            this.RepeatPassword = repeatPassword;
            this.RoleId = roleId;
            this.Cellphone = cellphone;
            this.Email = email;
            this.IsEnable = isEnable;
        }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public int RoleId { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }

    }
}
