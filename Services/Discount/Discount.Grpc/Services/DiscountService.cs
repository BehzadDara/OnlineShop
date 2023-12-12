using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService(ICouponRepository _couponRepository, IMapper _mapper, ILogger<DiscountService> _logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _couponRepository.GetByProductName(request.ProductName);

        _logger.LogInformation($"Get discount with product name {request.ProductName}");

        return _mapper.Map<CouponModel>(coupon);
    }

    public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        await _couponRepository.Create(coupon);

        _logger.LogInformation($"Create discount with product name {coupon.ProductName}");

        return _mapper.Map<CouponModel>(coupon);
    }

    public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        await _couponRepository.Update(coupon);

        _logger.LogInformation($"Update discount with product name {coupon.ProductName}");

        return _mapper.Map<CouponModel>(coupon);
    }

    public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var result = await _couponRepository.Delete(request.ProductName);

        _logger.LogInformation($"Delete discount with product name {request.ProductName}");

        return new DeleteDiscountResponse
        {
            Success = result,
        };
    }
}
