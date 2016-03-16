﻿using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Web.Core.Data
{
    [Component(typeof(IUnitOfWork), LifeTime.Hierarchical)]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EfDbContext _dbContext;
        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext as EfDbContext;
        }
        public bool Committed { get; private set; }

        public void Commit()
        {
            if (Committed) return;
            _dbContext.SaveChanges();
            Committed = true;
        }

        public void Rollback()
        {
            Committed = false;
        }
    }
}
