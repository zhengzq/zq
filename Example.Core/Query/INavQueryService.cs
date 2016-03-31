using System.Collections.Generic;
using System.Linq;
using Example.Core.Domain.Navigations;
using Zq;

namespace Example.Core.Query
{
    public interface INavQueryService : IQueryService
    {
        List<NavigationRecord> GetModules();
        List<NavigationRecord> GetNavigationRecords();
    }
    public class NavQueryService : INavQueryService
    {
        public List<NavigationRecord> GetModules()
        {
            var list = GetNavigationRecords().ToList();
            var m = list.Where(x => string.IsNullOrEmpty(x.ParentId)).OrderBy(x => x.Order).ToList();
            m.ForEach(x =>
            {
                list.ForEach(y =>
                {
                    if (y.ParentId == x.NavigationId)
                        x.Childs.Add(y);
                });
            });
            return m;
        }

        public List<NavigationRecord> GetNavigationRecords()
        {
            var provider = new StandardNavigationProvider();
            return provider.GetNavigationRecords().ToList();
        }
    }
}
