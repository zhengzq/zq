using System.Collections.Generic;
using Zq.Domain;

namespace Example.Web.Core.Domain.Permissions
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        List<Permission> GetPermissionsByNavSystemName(string nvaSystemName);
        List<Permission> GetAllPermission();
        bool Authorize(string permissionCode, int managerId);
    }
}
