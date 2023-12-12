using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IOrderRepository _orderRepository,
                                       IMapper _mapper,
                                       ILogger<UpdateOrderCommandHandler> _logger) : IRequestHandler<UpdateOrderCommand>
{
    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order is null)
        {
            _logger.LogError($"order with id {request.Id} does not exist");
            throw new NotFoundException(nameof(Order), request.Id);
        }

        _mapper.Map(request, order);

        await _orderRepository.UpdateAsync(order);

        _logger.LogInformation($"order by Id {order.Id} updated");
    }
}
