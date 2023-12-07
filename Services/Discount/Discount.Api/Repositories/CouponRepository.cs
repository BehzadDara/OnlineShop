using Dapper;
using Discount.Api.Entities;
using Npgsql;

namespace Discount.Api.Repositories;

public class CouponRepository(IConfiguration _configuration) : ICouponRepository
{
    public async Task<Coupon> GetByProductName(string productName)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ($"select * from Coupon where ProductName = '{productName}'");
        if (coupon is null)
        {
            return new Coupon
            {
                ProductName = productName,
                Description = "No discount",
                Amount = 0
            };
        }

        return coupon;
    }

    public async Task<bool> Create(Coupon coupon)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            ($"insert into Coupon(ProductName, Description, Amount) " +
            $"values('{coupon.ProductName}', '{coupon.Description}', {coupon.Amount})");

        return affected > 0;
    }

    public async Task<bool> Update(Coupon coupon)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            ($"update Coupon " +
            $"set ProductName = '{coupon.ProductName}', Description = '{coupon.Description}', Amount = {coupon.Amount} " +
            $"where Id = {coupon.Id}");

        return affected > 0;
    }

    public async Task<bool> Delete(string productName)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            ($"delete from Coupon where ProductName = {productName}");

        return affected > 0;
    }
}
