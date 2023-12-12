using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository _orderRepository,
                                       IMapper _mapper,
                                       IEmailService _emailService,
                                       ILogger<CreateOrderCommandHandler> _logger) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Order>(request);
        var result = await _orderRepository.CreateAsync(entity);

        _logger.LogInformation($"order by Id {result.Id} created");
        await SendMail(result);

        return result.Id;
    }

    private async Task SendMail(Order order)
    {
        try
        {
            await _emailService.SendEmailAsync(new Email
            {
                To = "Test@Test.com",
                Subject = "order added",
                Body = "email Body"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"email has not been sent, {ex.Message}");
        }
    }
}
