using Example.Core.Domain.Roles;
using Zq.UnitOfWork;

namespace Example.Core.Data
{
    public class RoleRepository : EfRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
