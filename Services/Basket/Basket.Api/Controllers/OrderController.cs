using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class OrderController(
        IOrderRepository _orderRepository, 
        DiscountGrpcService _discountService,
        IMapper _mapper,
        IPublishEndpoint _publishEndpoint
        ) : ControllerBase
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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _orderRepository.GetByUserName(basketCheckout.UserName);
            if (basket is null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);

            await _orderRepository.Delete(basketCheckout.UserName);

            return Accepted();
        }
    }
}
