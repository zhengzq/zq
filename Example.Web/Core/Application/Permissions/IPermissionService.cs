using System.Collections.Generic;
using Example.Web.Core.Domain.Permissions;
using Zq;

namespace Example.Web.Core.Application.Permissions
{
    public interface IPermissionService
    {
        void InitNavigationPermissionWithFunctionPermissionFromAssembly(List<string> navigationIds, string assemblyName);
        Permission GetPermissionById(string permissionId);
    }
}
