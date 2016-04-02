namespace Example.Core.Query.Dto
{
    public class ManagerDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }
    }
}
