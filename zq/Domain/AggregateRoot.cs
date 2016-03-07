
namespace Zq.Domain
{
    public abstract class AggregateRoot<TIdentity> : IAggregateRoot, IVersionable
    {
        public TIdentity Id { get; set; }

        public int Version { get; set; }
    }
}
