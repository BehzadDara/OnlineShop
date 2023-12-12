using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler(IOrderRepository _orderRepository, IMapper _mapper) : IRequestHandler<GetOrdersListQuery, List<OrderViewModel>>
{
    public async Task<List<OrderViewModel>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.GetOrdersByUserName(request.UserName);
        return _mapper.Map<List<OrderViewModel>>(result);
    }
}
