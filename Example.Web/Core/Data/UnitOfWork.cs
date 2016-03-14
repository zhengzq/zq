using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Web.Core.Data
{
    [Component(typeof(IUnitOfWork), LifeTime.Hierarchical)]
    public class UnitOfWork : IUnitOfWork
    {
        public bool Committed { get; }
        public void Commit()
        {
        }

        public void Rollback()
        {
        }
    }
}
