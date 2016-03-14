using System.Collections.Generic;

namespace Example.Web.Core.Application.Navigations
{
    public class NavigationRecord
    {
        public NavigationRecord()
        {
            this.Childs = new List<NavigationRecord>();
        }
        public string NavigationId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ParentId { get; set; }
        public int Order { get; set; }
        public bool Show { get; set; } = true;

        public List<NavigationRecord> Childs { get; set; }
    }
}