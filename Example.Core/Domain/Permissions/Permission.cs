using Zq.Domain;

namespace Example.Core.Domain.Permissions
{

    public class Permission : AggregateRoot<string>
    {
        public Permission() { }

        public Permission(string permissionId
            , string name
            , string navigationId
            , int order
            , bool isEnable
            , PermissionType type)
        {
            this.Id = permissionId;
            this.Name = name;
            this.NavigationId = navigationId;
            this.Order = order;
            this.IsEnable = isEnable;
            this.Type = type;
        }
        public string Name { get; set; }
        public string NavigationId { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public PermissionType Type { get; set; }
    }
}
