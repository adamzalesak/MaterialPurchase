using MaterialPurchase.Common.Application.CommandsAndQueries;
using MaterialPurchase.Orders.Infrastructure.Authorization;
using MaterialPurchase.OrdersContracts.Commands.CreateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace MaterialPurchase.Orders.Api;

[ApiController]
[Route("orders")]
public class OrdersController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    [Authorize(OrdersPolicies.CreateOrder)]
    public async Task<ActionResult> CreateOrder(CancellationToken cancellationToken)
    {
        await bus.InvokeCommandAsync(new CreateOrderCommand(), cancellationToken);
        return Ok();
    }
}