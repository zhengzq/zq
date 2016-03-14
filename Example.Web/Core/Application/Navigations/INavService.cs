using System.Collections.Generic;
using Zq;

namespace Example.Web.Core.Application.Navigations
{
    public interface INavService
    {
        OperateResult Create(CreateNavCommand command);
        OperateResult Update(UpdateNavCommand command);
        OperateResult Delete(List<string> navigationIds);
        List<NavigationRecord> GetModules();
        List<NavigationRecord> GetNavigationRecords();
    }
}
