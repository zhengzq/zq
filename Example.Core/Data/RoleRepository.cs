using Example.Core.Domain.Roles;
using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Core.Data
{
    [Component(typeof(IRoleRepository), LifeTime.Hierarchical)]
    public class RoleRepository : EfRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
