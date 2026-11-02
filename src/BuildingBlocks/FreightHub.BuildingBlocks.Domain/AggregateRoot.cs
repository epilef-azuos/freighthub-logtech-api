using FreightHub.Shipments.Domain;

namespace FreightHub.BuildingBlocks.Domain;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    // Lista exposta apenas para leitura
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Fundamental para Consistência Eventual e concorrência (Optimistic Concurrency)
    public int Version { get; protected set; }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void IncrementVersion()
    {
        Version++;
    }
}