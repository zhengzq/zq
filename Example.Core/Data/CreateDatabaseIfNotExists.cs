using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using Zq.DI;
using Zq.Logging;

namespace Example.Core.Data
{
    public sealed class CreateDatabaseIfNotExists : CreateDatabaseIfNotExists<EfDbContext>
    {
        protected override void Seed(EfDbContext context)
        {
            try
            {
                var filePath = ConfigurationManager.AppSettings["InitSqlFile"];
                if (string.IsNullOrEmpty(filePath))
                    filePath = "~/App_Data/InstallDBData.sql";
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
                Ioc.Resolve<ILogger>().Log(LogLevel.Error, ex);
                context.Database.Delete();
                throw ex;
            }

        }
    }
}
