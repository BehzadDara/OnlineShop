using OnlineShop.Aggregator.DTOs;

namespace OnlineShop.Aggregator.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseDTO>> GetOrderByUserName(string userName);
}
