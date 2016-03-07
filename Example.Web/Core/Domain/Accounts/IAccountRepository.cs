using Example.Web.Core.Domain.Managers;
using Zq.Domain;

namespace Example.Web.Core.Domain.Accounts
{
    public interface IAccountRepository : IRepository<Manager>
    {
        Account GetAccountByLoginName(string loginName);

        bool Validate(string loginName, string password);
    }
}
