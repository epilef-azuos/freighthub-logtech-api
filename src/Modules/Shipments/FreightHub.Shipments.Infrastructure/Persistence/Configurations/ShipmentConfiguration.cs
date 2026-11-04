using FreightHub.Shipments.Domain.Entities;
using FreightHub.Shipments.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightHub.Shipments.Infrastructure.Persistence.Configurations;

internal class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        // Define que este módulo gravará no seu próprio schema
        builder.ToTable("Shipments", "shipments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ShipperId)
            .IsRequired();

        builder.Property(x => x.CarrierId)
            .IsRequired(false);

        // Converte o Enum (inteiro) para String na base de dados para facilitar a leitura humana
        builder.Property(x => x.Status)
            .HasConversion(
                v => v.ToString(),
                v => (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus), v))
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.ProofOfDeliveryDocument)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(x => x.Version)
            .IsConcurrencyToken(); // Essencial para evitar concorrência (Optimistic Concurrency)

        // Ignora a coleção de eventos de domínio na criação da tabela
        builder.Ignore(x => x.DomainEvents);
    }
}
