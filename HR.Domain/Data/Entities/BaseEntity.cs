using HR.Domain.Abstractions;

namespace HR.Domain.Data.Entities
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public int Id { get; init; }
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();

        public void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent ==  null) throw new ArgumentNullException(nameof(domainEvent));
            _domainEvents.Add(domainEvent);
        }
    }
}
