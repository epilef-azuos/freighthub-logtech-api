using FreightHub.Shipments.Application.Interfaces;
using FreightHub.Shipments.Domain.Entities;
using MediatR;

namespace FreightHub.Shipments.Application.Commands.CreateShipment;

internal sealed class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, Guid>
{
    private readonly IShipmentWriteRepository _repository;

    public CreateShipmentCommandHandler(IShipmentWriteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
    {
        // 1. Instanciação do Domínio (A máquina de estados já garante que começa como 'Created')
        var shipment = new Shipment(request.ShipperId);

        // 2. Adição ao repositório de escrita (ainda em memória do EF Core)
        await _repository.AddAsync(shipment, cancellationToken);

        // 3. Persistência na base de dados
        // Nota Arquitetural: Quando implementarmos o Outbox, este SaveChangesAsync
        // irá disparar o envio das mensagens para a tabela de integração.
        await _repository.SaveChangesAsync(cancellationToken);

        return shipment.Id;
    }
}
