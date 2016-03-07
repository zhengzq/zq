using System;
using System.Collections.Generic;
using System.Linq;
using Zq.Domain;

namespace Example.Web.Core.Domain.Roles
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

        public List<RolePermission> RolePermissions { get; set; }

        public void SetPermission(int permissionId, string permissionCode)
        {
            if (RolePermissions == null)
                RolePermissions = new List<RolePermission>();
            this.RolePermissions.Add(new RolePermission(this.Id, permissionId, permissionCode));
        }

        public void ClearPermission()
        {
            this.RolePermissions.Clear();
        }
    }
}
