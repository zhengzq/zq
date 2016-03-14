using System;
using System.Collections.Generic;

namespace Example.Web.Core.Application.Navigations
{
    public class UpdateNavCommand
    {      
        public string NavigationId { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int Order { get; set; }
        public bool IsHide { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// Id ,Selected, Name
        /// </summary>
        public List<Tuple<int,bool,string>> Permissions { get; set; }
    }
 

}
