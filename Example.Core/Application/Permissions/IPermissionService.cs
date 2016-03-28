using System.Collections.Generic;
using Example.Core.Domain.Permissions;

namespace Example.Core.Application.Permissions
{
    public interface IPermissionService
    {
        void InitNavigationPermissionWithFunctionPermissionFromAssembly(List<string> navigationIds, string assemblyName);
        Permission GetPermissionById(string permissionId);
    }
}
