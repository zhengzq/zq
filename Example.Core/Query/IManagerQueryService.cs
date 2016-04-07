using Example.Core.Query.Dto;
using Example.Core.Query.Options;
using Zq;
using Zq.Paging;

namespace Example.Core.Query
{
    public interface IManagerQueryService : IQueryService
    {
        IPagedList<dynamic> Find(int index, int size, ManagerOption option);

        ManagerDto FindById(int id);

        ManagerDto FindByLoginName(string loginName);
    }
    public class ManagerQueryService : IManagerQueryService
    {

        public IPagedList<dynamic> Find(int index, int size, ManagerOption option)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT [Id]=[Manager].[Id]
      ,[LoginName]=[Manager].[LoginName]
      ,[UserName]=[Manager].[UserName]
      ,[RoleName]=[Role].Name
      ,[RoleId]=[Manager].[RoleId]
      ,[Cellphone]=[Manager].[Cellphone]
      ,[Email]=[Manager].[Email]
      ,[IsEnable]=[Manager].[IsEnable]
      ,[CreatedTime]=[Manager].[CreatedTime]
      ,[IsSys]=[Manager].[IsSys]
            FROM [Manager] WITH(NOLOCK) 
            LEFT JOIN [Role] WITH(NOLOCK) ON [Manager].RoleId=[Role].Id");

            sql.OrderBy("Manager.CreatedTime");
            var data = db.Page<dynamic>(index, size, sql);
            return new PagedList<dynamic>(data.Items, index, size);
        }


        public ManagerDto FindById(int id)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT [Id]=[Manager].[Id]
      ,[LoginName]=[Manager].[LoginName]
      ,[UserName]=[Manager].[UserName]
      ,[RoleName]=[Role].Name
      ,[RoleId]=[Manager].[RoleId]
      ,[Cellphone]=[Manager].[Cellphone]
      ,[Email]=[Manager].[Email]
      ,[IsEnable]=[Manager].[IsEnable]
      ,[CreatedTime]=[Manager].[CreatedTime]
      ,[IsSys]=[Manager].[IsSys]
  FROM [Manager] WITH(NOLOCK) 
  LEFT JOIN [Role] WITH(NOLOCK) ON [Manager].RoleId=[Role].Id WHERE [Manager].[Id]=@0", id);
            return db.SingleOrDefault<ManagerDto>(sql);
        }

        public ManagerDto FindByLoginName(string loginName)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT [Id]=[Manager].[Id]
      ,[LoginName]=[Manager].[LoginName]
      ,[UserName]=[Manager].[UserName]
      ,[RoleName]=[Role].Name
      ,[RoleId]=[Manager].[RoleId]
      ,[Cellphone]=[Manager].[Cellphone]
      ,[Email]=[Manager].[Email]
      ,[IsEnable]=[Manager].[IsEnable]
      ,[CreatedTime]=[Manager].[CreatedTime]
      ,[IsSys]=[Manager].[IsSys]
  FROM [Manager] WITH(NOLOCK) 
  LEFT JOIN [Role] WITH(NOLOCK) ON [Manager].RoleId=[Role].Id WHERE [Manager].LoginName=@0", loginName);
            return db.SingleOrDefault<ManagerDto>(sql);
        }
    }
}
