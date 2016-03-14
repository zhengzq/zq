namespace Example.Web.Core.Domain.Roles
{
    public class RolePermission
    {
        public RolePermission() { }

        public RolePermission(int roleId, string permissionId)
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string PermissionId { get; set; }
    }
}
