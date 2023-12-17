using Microsoft.AspNetCore.Mvc;
using OnlineShop.Aggregator.DTOs;
using OnlineShop.Aggregator.Services;
using System.Net;

namespace OnlineShop.Aggregator.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class ShoppingController(
    ICatalogService _catalogService,
    IBasketService _basketService,
    IOrderService _orderService
    ) : ControllerBase
{
    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(ShoppingDTO), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingDTO>> GetByUserName(string userName)
    {
        var basket = await _basketService.GetBasket(userName);
        foreach (var basketItem in basket.Items)
        {
            var product = await _catalogService.GetCatalog(basketItem.ProductId);

            basketItem.ProductName = product.Name;
            basketItem.Category = product.Category;
            basketItem.Summary = product.Summary;
            basketItem.Description = product.Description;
            basketItem.ImageFile = product.ImageFile;
        }

        var orders = await _orderService.GetOrderByUserName(userName);

        var shoppingDTO = new ShoppingDTO
        {
            UserName = userName,
            BasketWithProduct = basket,
            Orders = orders 
        };

        return Ok(shoppingDTO);
    }
}
