namespace Zq.Domain
{
    public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>, IAggregateRoot, IVersionable
    {
        public int Version { get; set; }
    }
}
