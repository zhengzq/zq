using System.Collections.Generic;
using Example.Web.Core.Domain.Permissions;
using Zq;

namespace Example.Web.Core.Application.Permissions
{
    public interface IPermissionService
    {
        void InitFunctionPermissionFromAssembly(string assemblyName);
        void InitNavigationPermission(List<string> navigationIds);
        Permission GetPermissionById(string permissionId);
    }
}
