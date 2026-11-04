using FreightHub.Shipments.Domain.Entities;

namespace FreightHub.Shipments.Application.Interfaces;

public interface IShipmentWriteRepository
{
    Task AddAsync(Shipment shipment, CancellationToken cancellationToken);
    Task<Shipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Update(Shipment shipment);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}