using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderDBContextSeed
{
    public static async Task SeedAsync(OrderDBContext orderDBContext, ILogger<OrderDBContextSeed> logger)
    {
        if (!await orderDBContext.Orders.AnyAsync())
        {
            await orderDBContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
            await orderDBContext.SaveChangesAsync();

            logger.LogInformation("data seed section configured");
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new() {
                UserName = "U1",
                FirstName = "F1",
                LastName = "D1",
                EmailAddress = "E1@e.com",
                Country = "Co1",
                City = "ci1",
                TotalPrice = 10000
            },
            new() {
                UserName = "U2",
                FirstName = "F2",
                LastName = "D2",
                EmailAddress = "E2@e.com",
                Country = "Co2",
                City = "ci2",
                TotalPrice = 20000
            }
        };
    }
}
