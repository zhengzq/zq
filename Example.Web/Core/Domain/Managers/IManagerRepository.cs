using Zq.Domain;

namespace Example.Web.Core.Domain.Managers
{
    public interface IManagerRepository : IRepository<Manager>
    {
        bool CheckLoginName(string loginName);
    }
}
