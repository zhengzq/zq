using Zq.Domain;

namespace Example.Web.Core.Domain.Permissions
{

    public class Permission : AggregateRoot<int>
    {
        public Permission() { }

        public Permission(string code
            , string name
            , string navSystemName
            , int order
            , bool isEnable
            , PermissionType type)
        {
            this.Name = name;
            this.NavSystemName = navSystemName;
            this.Order = order;
            this.IsEnable = isEnable;
            this.Type = type;
            this.Code = code;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NavSystemName { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public PermissionType Type { get; set; }
    }
}
