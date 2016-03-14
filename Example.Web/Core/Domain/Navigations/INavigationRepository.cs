using System.Collections.Generic;
using Zq.Domain;

namespace Example.Web.Core.Domain.Navigations
{
    public interface INavigationRepository : IRepository<Navigation>
    {
        bool CheckHaveChildNode(Navigation value);
        bool CheckExsitId(string navigationId);
        List<Navigation> GetAllNavigation();
    }
}
