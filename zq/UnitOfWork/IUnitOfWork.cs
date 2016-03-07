namespace Zq.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool Committed { get; }
        void Commit();
        void Rollback();
    }
}
