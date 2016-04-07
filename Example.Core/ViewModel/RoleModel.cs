using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Example.Core.Web.Extensions;

namespace Example.Core.ViewModel
{
    public class RoleModel 
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }
        [DisplayName("角色名称")]
        public string Name { get; set; }
        [DisplayName("排序")]
        [UIHint("PhoneNumber")]
        public int Order { get; set; }
        public List<NavModel> Navigations { get; set; }
    }
}