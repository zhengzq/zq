using System;
using Example.Web.Core.Domain.Permissions;

namespace Example.Web.Core.Application.Permissions
{
    public class PermissionDescriptionAttribute :Attribute
    {
        public PermissionDescriptionAttribute(string permissionId
            ,string name
            , string navigationId
            , PermissionType type = PermissionType.功能
            , bool isEnable = true
            , int order = 0)
        {
            this.PermissionId = permissionId;
            this.Name = name;
            this.NavigationId = navigationId;
            this.Type = type;
            this.IsEnable = isEnable;
            this.Order = order;
        }

        public string PermissionId { get; set; }
        public string Name { get; set; }
        public string NavigationId { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public PermissionType Type { get; set; }
    }
}
