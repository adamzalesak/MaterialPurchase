using MaterialPurchase.OrdersContracts.Commands.CreateOrder;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace MaterialPurchase.Orders.Api;

[ApiController]
[Route("orders")]
public class OrdersController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateOrder(CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new CreateOrderCommand(), cancellationToken);
        return Ok();
    }
}