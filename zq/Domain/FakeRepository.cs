using System.Diagnostics;

namespace Zq.Domain
{
    public class FakeRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot, new()
    {
        public virtual TAggregateRoot Get(object id)
        {
            Debug.WriteLine("Name:{0} Method:{1}", this.GetType().FullName, "Get");
            return default(TAggregateRoot);
        }

        public virtual void Add(TAggregateRoot aggregateRoot)
        {
            Debug.WriteLine("Name:{0} Method:{1}", this.GetType().FullName, "Add");
        }

        public virtual void Update(TAggregateRoot aggregateRoot)
        {
            Debug.WriteLine("Name:{0} Method:{1}", this.GetType().FullName, "Update");
        }

        public virtual void Delete(TAggregateRoot aggregateRoot)
        {
            Debug.WriteLine("Name:{0} Method:{1}", this.GetType().FullName, "Delete");
        }
    }
}
