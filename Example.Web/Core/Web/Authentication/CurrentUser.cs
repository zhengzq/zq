using Newtonsoft.Json;

namespace Example.Web.Core.Web.Authentication
{

    public class CurrentUser
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RealName { get; set; }
        public bool IsSys { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static CurrentUser FromJson(string json)
        {
            return JsonConvert.DeserializeObject<CurrentUser>(json);
        }
    }
}