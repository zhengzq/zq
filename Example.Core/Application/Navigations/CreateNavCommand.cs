namespace Example.Core.Application.Navigations
{
    public class CreateNavCommand
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
    }
}
