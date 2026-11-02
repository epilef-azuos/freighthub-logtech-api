using FluentAssertions;
using FreightHub.Shipments.Domain.Entities;
using FreightHub.Shipments.Domain.Enums;

namespace FreightHub.UnitTests.Modules.Shipments.Domain.Entities;

public class ShipmentTests
{
    [Fact]
    public void Constructor_Should_Initialize_In_Created_Status_And_Raise_Event()
    {
        // Arrange
        var shipperId = Guid.NewGuid();

        // Act
        var shipment = new Shipment(shipperId);

        // Assert
        shipment.ShipperId.Should().Be(shipperId);
        shipment.Status.Should().Be(ShipmentStatus.Created);
        shipment.Version.Should().Be(1);
        shipment.DomainEvents.Should().ContainSingle(e => e.GetType().Name == "ShipmentCreatedDomainEvent");
    }

    [Fact]
    public void Cancel_WhenInTransit_Should_Throw_InvalidOperationException()
    {
        // Arrange
        var shipment = new Shipment(Guid.NewGuid());
        shipment.AssignCarrier(Guid.NewGuid());
        shipment.PickUp();
        shipment.Depart(); // Status avança para InTransit

        // Act
        Action act = () => shipment.Cancel();

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Não é possível cancelar uma remessa que já está em trânsito ou entregue.");
    }

    [Fact]
    public void AssignCarrier_WhenStatusIsNotCreated_Should_Throw_InvalidOperationException()
    {
        // Arrange
        var shipment = new Shipment(Guid.NewGuid());
        shipment.AssignCarrier(Guid.NewGuid()); // Status muda para Assigned

        // Act
        Action act = () => shipment.AssignCarrier(Guid.NewGuid());

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Apenas remessas com status 'Created' podem ser atribuídas.");
    }
}
