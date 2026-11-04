using MediatR;

namespace FreightHub.Shipments.Application.Commands.CreateShipment;

public record CreateShipmentCommand(Guid ShipperId) : IRequest<Guid>;
