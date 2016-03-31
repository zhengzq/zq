using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Example.Core.Domain.Navigations;
using Zq.UnitOfWork;

namespace Example.Core.Data
{
    public class NavigationRepository : EfRepository<Navigation>, INavigationRepository
    {
        public NavigationRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public bool CheckHaveChildNode(Navigation value)
        {
            var parameter = new SqlParameter("@0", value.Id);
            var i = Context.Database.ExecuteSqlCommand("SELECT COUNT(0) FROM Navigation WHERE ParentId=@0"
                , parameter);

            return i > 0;
        }

        public bool CheckExsitId(string navigationId)
        {
            return Table.Count(x => x.Id == navigationId) < 0;
        }


        public bool CheckExsitSystemName(string systemName)
        {
            var parameter = new SqlParameter("@0", systemName);
            var i = Context.Database.ExecuteSqlCommand("SELECT COUNT(0) FROM Navigation WHERE SystemName=@0"
                , parameter);
            return i > 0;
        }


        public List<Navigation> GetAllNavigation()
        {
            return Table.ToList();

        }

    }
}
