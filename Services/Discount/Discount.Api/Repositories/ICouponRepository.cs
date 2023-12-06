using Discount.Api.Entities;

namespace Discount.Api.Repositories;

public interface ICouponRepository
{
    Task<Coupon> GetByProductName(string productName);
    Task<bool> Create(Coupon coupon);
    Task<bool> Update(Coupon coupon);
    Task<bool> Delete(string productName);
}
