﻿using System.Data.SqlClient;
using System.Linq;
using Example.Core.Domain.Managers;
using Zq.UnitOfWork;

namespace Example.Core.Data
{
    public class ManagerRepository : EfRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public bool CheckLoginName(string loginName)
        {
            var parameter = new SqlParameter("@0", loginName);
            var i = base.Context.Database.ExecuteSqlCommand("SELECT COUNT(0) FROM Manager WHERE LoginName=@0", parameter);
            return i > 0;
        }

        public Manager GetManagerByLoginName(string loginName)
        {
            return Table.FirstOrDefault(x => x.LoginName == loginName);
        }
    }
}
