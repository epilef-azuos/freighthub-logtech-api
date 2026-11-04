using FreightHub.Shipments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreightHub.Shipments.Infrastructure.Persistence;

public class ShipmentsDbContext : DbContext
{
    public DbSet<Shipment> Shipments => Set<Shipment>();

    public ShipmentsDbContext(DbContextOptions<ShipmentsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define o schema padrão para este contexto
        modelBuilder.HasDefaultSchema("shipments");

        // Aplica todas as configurações que implementam IEntityTypeConfiguration automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShipmentsDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
