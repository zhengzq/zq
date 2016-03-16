using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;

namespace Example.Web.Core.Data
{
    public sealed class CreateDatabaseIfNotExists : CreateDatabaseIfNotExists<EfDbContext>
    {
        protected override void Seed(EfDbContext context)
        {
            try
            {
                var filePath = ConfigurationManager.AppSettings["InitSqlFile"];
                if (string.IsNullOrEmpty(filePath)) throw new Exception("初始化数据文件不能为空!");
                if (filePath.StartsWith("~"))
                {
                    filePath = filePath.Substring(2);
                    filePath = $"{AppDomain.CurrentDomain.BaseDirectory}{filePath}";
                }
                var sql = File.ReadAllText(filePath);
                context.Database.ExecuteSqlCommand(sql);
                base.Seed(context);
            }
            catch (Exception ex)
            {
               
            }
            
        }
    }
}
