using FluentValidation;

namespace FreightHub.Shipments.Application.Commands.CreateShipment;

public class CreateShipmentCommandValidator : AbstractValidator<CreateShipmentCommand>
{
    public CreateShipmentCommandValidator()
    {
        RuleFor(x => x.ShipperId)
            .NotEmpty()
            .WithMessage("O identificador do embarcador (ShipperId) é obrigatório.");
    }
}
