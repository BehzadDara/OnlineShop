using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CouponController(ICouponRepository _couponRepository) : ControllerBase
    {
        [HttpGet("{productName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetByProductName(string productName)
        {
            var result = await _couponRepository.GetByProductName(productName);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Create([FromBody] Coupon coupon)
        {
            await _couponRepository.Create(coupon);
            return await GetByProductName(coupon.ProductName);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Update([FromBody] Coupon coupon)
        {
            await _couponRepository.Update(coupon);
            return await GetByProductName(coupon.ProductName);
        }

        [HttpDelete("{productName}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Delete(string productName)
        {
            var result = await _couponRepository.Delete(productName);
            return Ok(result);
        }
    }
}
