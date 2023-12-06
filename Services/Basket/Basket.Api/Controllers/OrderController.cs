using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class OrderController(IOrderRepository _orderRepository) : ControllerBase
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
