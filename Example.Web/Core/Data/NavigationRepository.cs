using System.Collections.Generic;
using Example.Web.Core.Domain.Navigations;
using Zq.Domain;
using Zq.Ioc;

namespace Example.Web.Core.Data
{
    [Component(typeof(INavigationRepository), LifeTime.Hierarchical)]
    public class NavigationRepository : FakeRepository<Navigation>, INavigationRepository
    {
        public bool CheckHaveChildNode(Navigation value)
        {
            return true;
        }

        public bool CheckExsitId(string navigationId)
        {
            return true;
        }

        public List<Navigation> GetAllNavigation()
        {
            return new List<Navigation>();
        }
    }
}
