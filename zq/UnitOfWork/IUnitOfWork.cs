using System;

namespace Zq.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool Committed { get; }
        void Commit();
        void Rollback();
    }
}
