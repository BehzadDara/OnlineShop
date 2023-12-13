using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.Api.EventBusConsumer;

public class BasketCheckoutConsumer(
    IMapper _mapper, 
    IMediator _mediator, 
    ILogger<BasketCheckoutConsumer> _logger
    ) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var createOrderCommand = _mapper.Map<CreateOrderCommand>(context.Message);
        var result = await _mediator.Send(createOrderCommand);
        _logger.LogInformation($"Checkout basket consumed by Order service with result Id = {result}");
    }
}
