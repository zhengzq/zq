using System.Collections.Generic;
using Zq.Domain;

namespace Example.Core.Domain.Roles
{
    public class Role : AggregateRoot<int>
    {
        public Role() { }

        public Role(string name, int order)
        {
            this.Name = name;
            this.Order = order;
        }

        public string Name { get; set; }
        public int Order { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }

        public void SetPermission(string permissionId)
        {
            if (RolePermissions == null)
                RolePermissions = new List<RolePermission>();
            this.RolePermissions.Add(new RolePermission(this.Id, permissionId));
        }

        public void ClearPermission()
        {
            this.RolePermissions.Clear();
        }
    }
}
