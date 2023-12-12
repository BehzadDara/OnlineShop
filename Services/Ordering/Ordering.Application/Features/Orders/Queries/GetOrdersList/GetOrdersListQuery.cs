using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public sealed record GetOrdersListQuery(string UserName) : IRequest<List<OrderViewModel>>;
