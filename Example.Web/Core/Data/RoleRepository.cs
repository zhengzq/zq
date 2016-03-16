using Example.Web.Core.Domain.Roles;
using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Web.Core.Data
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
