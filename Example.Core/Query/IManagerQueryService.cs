using System.Collections.Generic;
using Example.Core.Query.Dto;
using Example.Core.Query.Options;
using Zq;
using Zq.Paging;

namespace Example.Core.Query
{
    public interface IManagerQueryService : IQueryService
    {
        IPagedList<dynamic> Find(int index,int size, ManagerOption option);

        ManagerDto FindById(int id);
    }
    public class ManagerQueryService : IManagerQueryService
    {

        public IPagedList<dynamic> Find(int index, int size, ManagerOption option)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT [Manager].[Id]
                ,[LoginName]
                ,[UserName]
                ,[RoleId]
                ,[Cellphone]
                ,[Email]
                ,[IsEnable]
                ,RoleName=[Role].Name
                ,[CreatedTime]
            FROM [Manager] WITH(NOLOCK) 
            LEFT JOIN [Role] WITH(NOLOCK) ON [Manager].RoleId=[Role].Id");

            sql.OrderBy("Manager.CreatedTime");
            var data = db.Page<dynamic>(index, size, sql);
            return new PagedList<dynamic>(data.Items, index, size);
        }


        public ManagerDto FindById(int id)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT [Id]
      ,[LoginName]
      ,[UserName]
      ,[RoleId]
      ,[Cellphone]
      ,[Email]
      ,[IsEnable]
      ,[CreatedTime]
  FROM [Manager] WITH(NOLOCK) WHERE ID=@0", id);
            return db.SingleOrDefault<ManagerDto>(sql);
        }
    }
}
