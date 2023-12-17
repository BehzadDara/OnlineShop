using OnlineShop.Aggregator.DTOs;
using OnlineShop.Aggregator.Extensions;

namespace OnlineShop.Aggregator.Services;

public class BasketService(HttpClient _client) : IBasketService
{
    public async Task<BasketDTO> GetBasket(string userName)
    {
        var response = await _client.GetAsync($"/api/v1/Order/GetByUserName/{userName}");
        return await response.ReadContentAs<BasketDTO>();
    }
}
