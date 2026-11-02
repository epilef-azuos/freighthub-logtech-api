using MediatR;

namespace FreightHub.Shipments.Domain;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}
