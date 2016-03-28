using System.Collections.Generic;
using Zq;

namespace Example.Core.Application.Roles
{
    public interface IRoleService  
    {
        OperateResult Update(UpdateRoleCommand command);
        OperateResult Create(CreateRoleCommand command);
        OperateResult Delete(List<int> roleIds);
        /// <summary>
        /// 当前管理员是否有权限-Code
        /// </summary>
        bool CheckRoleIsAuthorized(string code, int roleId);
    }
  
}
