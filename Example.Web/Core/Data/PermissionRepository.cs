using System.Collections.Generic;
using Example.Web.Core.Domain.Permissions;
using Zq.Domain;
using Zq.Ioc;

namespace Example.Web.Core.Data
{
    [Component(typeof(IPermissionRepository), LifeTime.Hierarchical)]
    public class PermissionRepository : FakeRepository<Permission>, IPermissionRepository
    {
        public List<Permission> GetPermissionsByNavigationId(string navigationId)
        {
            return new List<Permission>();
        }

        public List<Permission> GetAllPermission()
        {
            return new List<Permission>();
        }

        public bool Authorize(string permissionId, int managerId)
        {
           return true;
        }
    }
}
