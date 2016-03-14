using Zq.Domain;

namespace Example.Web.Core.Domain.Navigations
{
    public class Navigation : AggregateRoot<string>
    {
        public Navigation() { }
        public Navigation(string navigationId
            , string parentId = ""
            , int order = 0
            , bool isHide = false
            , string name = ""
            , string icon = ""
            , string link = ""
            , string remark = ""
            , bool isSys = false)
        {
            this.Id = navigationId;
            this.Name = name;
            this.ParentId = parentId;
            this.Order = order;
            this.IsHide = isHide;
            this.Icon = icon;
            this.Link = link;
            this.Remark = remark;
            this.IsSys = isSys;
        }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int Order { get; set; }
        public bool IsHide { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public string Remark { get; set; }
        public bool IsSys { get; set; }
        public void CheckIsSys()
        {
            if (this.IsSys)
                throw new DomainException("系统导航不能被删除");
        }

    }
}
