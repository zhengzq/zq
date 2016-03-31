using System.Collections.Generic;
using Example.Core.Domain.Navigations;
using Zq;

namespace Example.Core.Application.Navigations
{
    public interface INavService : IApplicationService
    {
        OperateResult Create(CreateNavCommand command);
        OperateResult Update(UpdateNavCommand command);
        OperateResult Delete(List<string> navigationIds);
        List<NavigationRecord> GetModules();
        List<NavigationRecord> GetNavigationRecords();
    }
}
