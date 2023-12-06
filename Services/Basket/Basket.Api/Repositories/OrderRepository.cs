using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories;

public class OrderRepository(IDistributedCache _redisCache) : IOrderRepository
{
    public async Task<Order> GetByUserName(string userName)
    {
        var result = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(result))
        {
            return new Order(userName);
        }

        var order = JsonConvert.DeserializeObject<Order>(result);
        if (order is null)
        {
            return new Order(userName);
        }

        return order;
    }

    public async Task<Order> Update(Order order)
    {
        await _redisCache.SetStringAsync(order.UserName, JsonConvert.SerializeObject(order));
        return await GetByUserName(order.UserName);
    }
    public async Task Delete(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
}
