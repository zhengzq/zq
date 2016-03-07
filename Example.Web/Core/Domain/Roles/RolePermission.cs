namespace Example.Web.Core.Domain.Roles
{
    public class RolePermission
    {
        public RolePermission() { }

        public RolePermission(int roleId, int permissionId, string permissionCode)
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
            this.PermissionCode = permissionCode;
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionCode { get; set; }
    }
}
