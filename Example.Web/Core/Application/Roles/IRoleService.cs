using Zq;

namespace Example.Web.Core.Application.Roles
{
    public interface IRoleService  
    {
        OperateResult Update(UpdateRoleCommand command);
        OperateResult Create(CreateRoleCommand command);
        OperateResult Delete(DeleteRoleCommand command);
    }
  
}
