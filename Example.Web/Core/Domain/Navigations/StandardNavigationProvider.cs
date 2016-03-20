using System.Collections.Generic;

namespace Example.Web.Core.Domain.Navigations
{
    public class StandardNavigationProvider
    {
        internal static StandardNavigationProvider Instance => new StandardNavigationProvider();


        internal virtual List<NavigationRecord> GetNavigationRecords()
        {
            var list = new List<NavigationRecord>();

            #region 系统管理
            list.Add(new NavigationRecord
            {
                NavigationId = "ModuleSys", Title = "系统管理", Url = "", Order = 1,Icon = "fa-home"
            });
            list.Add(new NavigationRecord
            {
                NavigationId = "MenuManager", Title = "用户管理", Url = "/Admin/Manager/Index", ParentId = "ModuleSys"
            });
            list.Add(new NavigationRecord
            {
                NavigationId = "MenuRole", Title = "角色管理", Url = "/Role/Index", ParentId = "ModuleSys"
            });

            #endregion

            #region 工具
            list.Add(new NavigationRecord
            {
                NavigationId = "ModuleTool",
                Title = "工具",
                Url = "",
                Order = 2,
                Icon = "fa-cutlery"
            });
            list.Add(new NavigationRecord
            {
                NavigationId = "MenuFormBuild",
                Title = "表单构建器",
                Url = "/Admin/Tool/FormBuild",
                ParentId = "ModuleTool"
            });

            #endregion

            return list;
        }
    }
}
