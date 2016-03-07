using Example.Web.Core.Domain.Permissions;

namespace Example.Web.Core.Application.Permissions
{
    public class PermissionRecord
    {
        public PermissionRecord()
        {
        }

        public PermissionRecord(string code
              , string name
              , string navSystemName
              , PermissionType type = PermissionType.功能
              , bool isEnable = true
              , int order = 0)
        {
            this.Code = code;
            this.Name = name;
            this.NavSystemName = navSystemName;
            this.Type = type;
            this.IsEnable = isEnable;
            this.Order = order;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string NavSystemName { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public PermissionType Type { get; set; }
    }
}
