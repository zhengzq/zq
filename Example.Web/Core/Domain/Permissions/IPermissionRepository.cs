using System.Collections.Generic;
using Zq.Domain;

namespace Example.Web.Core.Domain.Permissions
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        List<Permission> GetPermissionsByNavigationId(string navigationId);
        List<Permission> GetAllPermission();
        bool Authorize(string permissionId, int managerId);
    }
}
