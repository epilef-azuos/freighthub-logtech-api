using FreightHub.Shipments.Application.Interfaces;
using FreightHub.Shipments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreightHub.Shipments.Infrastructure.Persistence;

internal sealed class ShipmentWriteRepository : IShipmentWriteRepository
{
    private readonly ShipmentsDbContext _context;

    public ShipmentWriteRepository(ShipmentsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Shipment shipment, CancellationToken cancellationToken)
    {
        await _context.Shipments.AddAsync(shipment, cancellationToken);
    }

    public async Task<Shipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Shipments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Update(Shipment shipment)
    {
        _context.Shipments.Update(shipment);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
