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
                NavigationId = "MenuUser", Title = "用户管理", Url = "/User/Index", ParentId = "ModuleSys"
            });
            list.Add(new NavigationRecord
            {
                NavigationId = "MenuRole", Title = "角色管理", Url = "/Role/Index", ParentId = "ModuleSys"
            });

            #endregion

            return list;
        }
    }
}
