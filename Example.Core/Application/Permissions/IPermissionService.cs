using System.Collections.Generic;
using Example.Core.Domain.Permissions;
using Zq;

namespace Example.Core.Application.Permissions
{
    public interface IPermissionService : IApplicationService
    {
        void InitNavigationPermissionWithFunctionPermissionFromAssembly(List<string> navigationIds, string assemblyName);
        Permission GetPermissionById(string permissionId);
    }
}
