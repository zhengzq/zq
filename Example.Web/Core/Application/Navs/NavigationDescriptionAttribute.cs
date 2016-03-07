using System;

namespace Example.Web.Core.Application.Navs
{
    public class NavigationDescriptionAttribute : Attribute
    {
        public NavigationDescriptionAttribute(string systemName
            , string fullNme
            , string icon
            , string link
            , bool isHide = true
            , bool isSys = true
            , string remark = "")
        {
            this.SystemName = systemName;
            this.FullName = fullNme;
            this.Icon = icon;
            this.Link = link;
            this.IsHide = isHide;
            this.IsSys = isSys;
            this.Remark = remark;
        }

        public string SystemName { get; set; }
        public string FullName { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }

        public bool IsHide { get; set; }
        public string Remark { get; set; }
        public bool IsSys { get; set; }
    }
}
