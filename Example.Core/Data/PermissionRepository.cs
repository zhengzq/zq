using System.Collections.Generic;
using System.Linq;
using Example.Core.Domain.Permissions;
using Zq.UnitOfWork;

namespace Example.Core.Data
{
    public class PermissionRepository : EfRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }

        public List<Permission> GetPermissionsByNavigationId(string navigationId)
        {
            return Table.Where(x => x.NavigationId == navigationId).ToList();
        }

        public List<Permission> GetAllPermission()
        {
            return Table.ToList();
        }

      
    }
}
