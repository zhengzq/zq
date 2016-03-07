using System.Collections.Generic;

namespace Example.Web.Core.Application.Roles
{
    public class DeleteRoleCommand
    {
        public DeleteRoleCommand(List<int> roleIds)
        {
            this.RoleIds = roleIds;
        }

        public List<int> RoleIds { get; set; }
    }
}
