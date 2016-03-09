namespace Zq.Domain
{
    public partial interface IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot, new()
    {
        TAggregateRoot Get(object id);
        void Add(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        void Delete(TAggregateRoot aggregateRoot);
    }
}
