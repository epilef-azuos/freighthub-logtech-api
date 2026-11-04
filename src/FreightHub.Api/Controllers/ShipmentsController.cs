using FreightHub.Shipments.Application.Commands.CreateShipment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreightHub.Api.Controllers;

[ApiController]
[Route("api/shipments")]
public class ShipmentsController : ControllerBase
{
    // Utilizar ISender em vez de IMediator respeita o Princípio de Segregação de Interface (ISP)
    private readonly ISender _sender;

    public ShipmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShipmentCommand command, CancellationToken cancellationToken)
    {
        var shipmentId = await _sender.Send(command, cancellationToken);

        return Accepted($"/api/shipments/{shipmentId}", new { Id = shipmentId, Message = "Remessa em processamento." });
    }
}
