using Zq.Domain;

namespace Example.Core.Domain.Managers
{
    public interface IManagerRepository : IRepository<Manager>
    {
        bool CheckLoginName(string loginName);
        Manager GetManagerByLoginName(string loginName);
    }
}
