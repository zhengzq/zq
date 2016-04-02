using System.Collections.Generic;
using Example.Core.Query.Dto;
using Example.Core.Query.Options;
using Zq;
using Zq.Paging;

namespace Example.Core.Query
{
    public interface IRoleQueryService : IQueryService
    {
        IPagedList<dynamic> Find(int index, int size, RoleOption option);
        List<RoleDto> Find();
        dynamic FindById(string id);
    }

    public class RoleQueryService : IRoleQueryService
    {

        public IPagedList<dynamic> Find(int index, int size, RoleOption option)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT  [Id]
      ,[Name]
      ,[Order]
  FROM [Role] WITH(NOLOCK)");

            sql.OrderBy("([Role].[Order]) ASC");
            var data = db.Page<dynamic>(index, size, sql);
            return new PagedList<dynamic>(data.Items, index, size);
        }


        public dynamic FindById(string id)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT  [Id]
      ,[Name]
      ,[Order]
  FROM [Role] WITH(NOLOCK) WHERE ID=@0", id);
            return db.SingleOrDefault<dynamic>(sql);
        }


        public List<RoleDto> Find()
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT  [Id]
      ,[Name]
      ,[Order]
  FROM [Role] WITH(NOLOCK)");
            return db.Fetch<RoleDto>(sql);
        }
    }
}
