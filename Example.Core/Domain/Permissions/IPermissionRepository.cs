using System.Collections.Generic;
using Zq.Domain;

namespace Example.Core.Domain.Permissions
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        List<Permission> GetPermissionsByNavigationId(string navigationId);
        List<Permission> GetAllPermission();
 
    }
}
