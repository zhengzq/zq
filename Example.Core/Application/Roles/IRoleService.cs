using System.Collections.Generic;
using Zq;

namespace Example.Core.Application.Roles
{
    public interface IRoleService : IApplicationService
    {
        OperateResult Update(UpdateRoleCommand command);
        OperateResult Create(CreateRoleCommand command);
        OperateResult Delete(List<int> roleIds);
        /// <summary>
        /// 当前管理员是否有权限-permissionId
        /// </summary>
        bool CheckRoleIsAuthorized(string permissionId, int roleId);
    }
  
}
