using System.Collections.Generic;

namespace Example.Core.ViewModel
{
    public class RoleModel : BaseModel
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public List<NavModel> Navigations { get; set; }
    }
}