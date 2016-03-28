namespace Example.Core.Application.Managers
{
    public class UpdateManagerCommand
    {
        public UpdateManagerCommand(int managerId
            ,string userName
            ,string password
            ,int roleId
            ,string cellphone
            ,string email
            ,bool isEnable)
        {
            this.ManagerId = managerId;
            this.UserName = userName;
            this.Password = password;
            this.RoleId = roleId;
            this.Cellphone = cellphone;
            this.Email = email;
            this.IsEnable = isEnable;
        }

        public string Password { get; set; }
        public int ManagerId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }
    }
}
