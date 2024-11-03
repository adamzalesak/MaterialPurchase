using MaterialPurchase.Common.Application.CommandsAndQueries;
using MaterialPurchase.OrderCarts.Application.Commands.CreateOrderCart;
using MaterialPurchase.OrderCarts.Application.Commands.FinishOrderCart;
using MaterialPurchase.OrderCarts.Application.Queries.GetOrderCart;
using MaterialPurchase.OrderCarts.Application.Queries.GetOrderCarts;
using MaterialPurchase.OrderCarts.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace MaterialPurchase.OrderCarts.Api;

[ApiController]
[Route("order-carts")]
public class OrderCartsController(IMessageBus bus) : ControllerBase
{
    [HttpPost("/")]
    public async Task<ActionResult<Guid>> CreateOrderCart([FromBody] CreateOrderCartRequest request, CancellationToken cancellationToken)
    {
        var id = await bus.InvokeCommandAsync(new CreateOrderCartCommand(request.Name), cancellationToken);

        return Ok(id);
    }

    [HttpPost("{id:guid}/finish")]
    public async Task<ActionResult> FinishOrderCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await bus.InvokeCommandAsync(new FinishOrderCartCommand(id), cancellationToken);

        return Ok();
    }
    
    [HttpGet]
    [Authorize(OrderCartsPolicies.GetOrderCarts)]
    public async Task<ActionResult<ICollection<GetOrderCartsResponse>>> GetOrderCarts(CancellationToken cancellationToken)
    {
        var result = await bus.InvokeQueryAsync(new GetOrderCartsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<GetOrderCartResponse?>> GetOrderCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await bus.InvokeQueryAsync(new GetOrderCartQuery(id), cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}