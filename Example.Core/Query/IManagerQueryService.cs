using System.Collections.Generic;
using Example.Core.Query.Options;
using Zq;
using Zq.Paging;

namespace Example.Core.Query
{
    public interface IManagerQueryService : IQueryService
    {
        IPagedList<dynamic> Find(int index,int size, ManagerOption option);

        dynamic FindById(string id);
    }
    public class ManagerQueryService : IManagerQueryService
    {

        public IPagedList<dynamic> Find(int index, int size, ManagerOption option)
        {
            //          var db = new ReadDbContext();
            //          var sql = new Sql(@"SELECT [Manager].[Id]
            //    ,[LoginName]
            //    ,[UserName]
            //    ,[RoleId]
            //    ,[Cellphone]
            //    ,[Email]
            //    ,[IsEnable]
            //    ,RoleName=[Role].Name
            //    ,[CreatedTime]
            //FROM [Manager] WITH(NOLOCK) 
            //LEFT JOIN [Role] WITH(NOLOCK) ON [Manager].RoleId=[Role].Id");

            //          sql.OrderBy("Manager.CreatedTime");
            //          var data = db.Page<dynamic>(option.PageIndex, option.PageSize, sql);
            //          return new PagedList<dynamic>(data.Items, option.PageIndex, option.PageSize);
            return new PagedList<dynamic>(new List<dynamic>()
            {
                new {LoginName="zzq",UserName="郑志强",CellPhone=110,RoleName="超级管理员",Id=1,Cellphone="1"},
                new {LoginName="zzq1",UserName="郑志强1",CellPhone=110,RoleName="超级管理员",Id=2,Cellphone="2"},
                new {LoginName="zzq2",UserName="郑志强2",CellPhone=110,RoleName="超级管理员",Id=3,Cellphone="3"},
            }, index, size);
        }


        public dynamic FindById(string id)
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
            return db.SingleOrDefault<dynamic>(sql);
        }
    }
}
