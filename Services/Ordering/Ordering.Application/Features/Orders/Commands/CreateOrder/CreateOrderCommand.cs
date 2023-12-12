using MediatR;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
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
    ) : IRequest<int>;