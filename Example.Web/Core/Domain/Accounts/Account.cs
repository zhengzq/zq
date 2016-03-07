using Zq.Domain;

namespace Example.Web.Core.Domain.Accounts
{
    public class Account
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public bool IsEnable { get; set; }
        public bool IsSys { get; set; }
        public void CheckAccount()
        {
            if (!this.IsEnable)
                throw new DomainException("账号已被禁用", 1);
        }
    }
}
