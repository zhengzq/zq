using Example.Web.Core.Domain.Roles;
using Zq.Domain;
using Zq.Ioc;

namespace Example.Web.Core.Data
{
    [Component(typeof(IRoleRepository), LifeTime.Hierarchical)]
    public class RoleRepository : FakeRepository<Role>, IRoleRepository
    {
    
    }
}
