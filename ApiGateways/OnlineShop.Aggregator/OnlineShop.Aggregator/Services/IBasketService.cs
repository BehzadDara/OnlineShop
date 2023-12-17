using OnlineShop.Aggregator.DTOs;

namespace OnlineShop.Aggregator.Services;

public interface IBasketService
{
    Task<BasketDTO> GetBasket(string userName);
}
