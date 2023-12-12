using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(int Id) : IRequest;