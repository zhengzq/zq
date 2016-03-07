using Zq.UnitOfWork;

namespace Zq.Repository.Ef
{
    public class UnitWork : IUnitOfWork
    {
        private readonly EfDbContext _dbContext;
        public UnitWork(EfDbContext dbContext)
        {
            _dbContext = dbContext;
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
