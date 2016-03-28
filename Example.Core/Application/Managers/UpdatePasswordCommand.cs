namespace Example.Core.Application.Managers
{
    public class UpdatePasswordCommand
    {
        public UpdatePasswordCommand(int managerId
            , string password
            , string repeatPassword)
        {
            this.ManagerId = managerId;
            this.Password = password;
            this.RepeatPassword = repeatPassword;
         
        }
        public int ManagerId { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
