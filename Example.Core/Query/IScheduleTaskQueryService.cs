using Example.Core.Query.Options;
using Zq;
using Zq.DI;
using Zq.Paging;

namespace Example.Core.Query
{
    public interface IScheduleTaskQueryService : IQueryService
    {
        IPagedList<dynamic> Find(int index, int size, ScheduleTaskOption option);
        dynamic FindById(string type);
    }

    public class ScheduleTaskQueryService : IScheduleTaskQueryService
    {

        public IPagedList<dynamic> Find(int index, int size, ScheduleTaskOption option)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT [Id]
      ,[Name]
      ,[Seconds]
      ,[Type]
      ,[Enabled]
      ,[StopOnError]
      ,[LastStartTime]
      ,[LastEndTime]
      ,[LastSuccessTime]
      ,[Arguments]
      ,[CreatedTime]
  FROM [ScheduleTask] WITH(NOLOCK)");

            sql.OrderBy("([ScheduleTask].[CreatedTime]) ASC");
            var data = db.Page<dynamic>(index, size, sql);
            return new PagedList<dynamic>(data.Items, index, size);
        }


        public dynamic FindById(string type)
        {
            var db = new ReadDbContext();
            var sql = new Sql(@"SELECT [Id]
      ,[Name]
      ,[Seconds]
      ,[Type]
      ,[Enabled]
      ,[StopOnError]
      ,[LastStartTime]
      ,[LastEndTime]
      ,[LastSuccessTime]
      ,[Arguments]
      ,[CreatedTime]
  FROM [ScheduleTask] WITH(NOLOCK) WHERE Id=@0", type);
            return db.SingleOrDefault<dynamic>(sql);
        }
    }
}
