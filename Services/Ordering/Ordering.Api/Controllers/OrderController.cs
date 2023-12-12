using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System.Net;

namespace Ordering.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class OrderController(IMediator _mediator) : ControllerBase
{
    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrdersByUserName(string userName)
    {
        var result = await _mediator.Send(new GetOrdersListQuery(userName));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        await _mediator.Send(new DeleteOrderCommand(id));
        return NoContent();
    }
}
