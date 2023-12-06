using Basket.Api.Entities;

namespace Basket.Api.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByUserName(string userName);
    Task<Order> Update(Order order);
    Task Delete(string userName);
}
