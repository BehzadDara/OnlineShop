using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderDBContext orderDBContext) 
    : RepositoryBase<Order>(orderDBContext), IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        return await _dbSet.Where(x => x.UserName == userName).ToListAsync();
    }
}
