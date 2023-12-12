using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices;

public class DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient _discountProtoService)
{
    public async Task<CouponModel> GetDiscount(string ProductName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = ProductName };
        return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
}
