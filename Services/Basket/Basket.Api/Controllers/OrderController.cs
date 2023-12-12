using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class OrderController(IOrderRepository _orderRepository, DiscountGrpcService _discountService) : ControllerBase
    {
        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> GetByUserName(string userName)
        {
            var result = await _orderRepository.GetByUserName(userName);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> Update([FromBody] Order order)
        {
            foreach (var orderItem in order.Items)
            {
                var coupon = await _discountService.GetDiscount(orderItem.ProductName);
                orderItem.Price -= coupon.Amount;
            }

            var result = await _orderRepository.Update(order);
            return Ok(result);
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string userName)
        {
            await _orderRepository.Delete(userName);
            return Ok();
        }
    }
}
