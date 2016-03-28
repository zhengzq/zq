using Zq.Domain;

namespace Example.Core.Domain.Roles
{
    public class RolePermission : Entity<int>
    {
        public RolePermission() { }

        public RolePermission(int roleId, string permissionId)
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }

        public int RoleId { get; set; }
        public string PermissionId { get; set; }
    }
}
