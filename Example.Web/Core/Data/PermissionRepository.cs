﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Example.Web.Core.Domain.Permissions;
using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Web.Core.Data
{
    [Component(typeof(IPermissionRepository), LifeTime.Hierarchical)]
    public class PermissionRepository : EfRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }

        public List<Permission> GetPermissionsByNavigationId(string navigationId)
        {
            return Table.Where(x => x.NavigationId == navigationId).ToList();
        }

        public List<Permission> GetAllPermission()
        {
            return Table.ToList();
        }

      
    }
}
