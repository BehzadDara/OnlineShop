using MediatR;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(
        int Id,
        string UserName,
        decimal TotalPrice,
        string FirstName,
        string LastName,
        string EmailAddress,
        string Country,
        string City,
        string BankName,
        string RefCode,
        int PaymentMethod
    ) : IRequest;