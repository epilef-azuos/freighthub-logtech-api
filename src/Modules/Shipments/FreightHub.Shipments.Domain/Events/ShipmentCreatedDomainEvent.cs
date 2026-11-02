namespace FreightHub.Shipments.Domain.Events;

public record ShipmentCreatedDomainEvent(Guid ShipmentId, Guid ShipperId) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
