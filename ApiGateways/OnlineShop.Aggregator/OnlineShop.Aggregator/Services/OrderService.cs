using OnlineShop.Aggregator.DTOs;
using OnlineShop.Aggregator.Extensions;

namespace OnlineShop.Aggregator.Services;

public class OrderService(HttpClient _client) : IOrderService
{
    public async Task<IEnumerable<OrderResponseDTO>> GetOrderByUserName(string userName)
    {
        var response = await _client.GetAsync($"/api/v1/Order/GetOrdersByUserName/{userName}");
        return await response.ReadContentAs<List<OrderResponseDTO>>();
    }
}
