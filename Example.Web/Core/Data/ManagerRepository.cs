using Example.Web.Core.Domain.Managers;
using Zq.Domain;
using Zq.Ioc;

namespace Example.Web.Core.Data
{
    [Component(typeof(IManagerRepository), LifeTime.Hierarchical)]
    public class ManagerRepository : FakeRepository<Manager>, IManagerRepository
    {
        public bool CheckLoginName(string loginName)
        {
            return true;
        }

        public Manager GetManagerByLoginName(string loginName)
        {
            return new Manager();
        }

        public bool Validate(string loginName, string password)
        {
            return true;
        }
    }
}
