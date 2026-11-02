using FreightHub.Shipments.Domain.Enums;
using FreightHub.Shipments.Domain.Events;
using FreightHub.BuildingBlocks.Domain;

namespace FreightHub.Shipments.Domain.Entities;

public class Shipment : AggregateRoot
{
    public Guid ShipperId { get; private set; }
    public Guid? CarrierId { get; private set; }
    public ShipmentStatus Status { get; private set; }
    public string? ProofOfDeliveryDocument { get; private set; }

    // Construtor privado para o EF Core
    private Shipment() { }

    public Shipment(Guid shipperId)
    {
        Id = Guid.NewGuid();
        ShipperId = shipperId;
        Status = ShipmentStatus.Created;
        Version = 1; // Inicia a versão do agregado

        AddDomainEvent(new ShipmentCreatedDomainEvent(Id, ShipperId));
    }

    public void AssignCarrier(Guid carrierId)
    {
        if (Status != ShipmentStatus.Created)
            throw new InvalidOperationException("Apenas remessas com status 'Created' podem ser atribuídas.");

        CarrierId = carrierId;
        Status = ShipmentStatus.Assigned;

        IncrementVersion(); // Cada mudança de estado incrementa a versão
        // TODO: AddDomainEvent(new ShipmentAssignedDomainEvent(Id, carrierId));
    }

    public void PickUp()
    {
        if (Status != ShipmentStatus.Assigned)
            throw new InvalidOperationException("A remessa precisa estar atribuída a uma transportadora antes da coleta.");

        Status = ShipmentStatus.PickedUp;
    }

    public void Depart()
    {
        if (Status != ShipmentStatus.PickedUp)
            throw new InvalidOperationException("A remessa precisa ser coletada antes de entrar em trânsito.");

        Status = ShipmentStatus.InTransit;
    }

    public void Deliver(string proofOfDeliveryDocument)
    {
        if (Status != ShipmentStatus.InTransit)
            throw new InvalidOperationException("Apenas remessas em trânsito podem ser entregues.");

        if (string.IsNullOrWhiteSpace(proofOfDeliveryDocument))
            throw new ArgumentException("O comprovante de entrega é obrigatório.", nameof(proofOfDeliveryDocument));

        ProofOfDeliveryDocument = proofOfDeliveryDocument;
        Status = ShipmentStatus.Delivered;
    }

    public void Cancel()
    {
        // Regra: Só é possível cancelar antes de InTransit.
        if (Status == ShipmentStatus.InTransit || Status == ShipmentStatus.Delivered)
            throw new InvalidOperationException("Não é possível cancelar uma remessa que já está em trânsito ou entregue.");

        Status = ShipmentStatus.Cancelled;
    }
}
