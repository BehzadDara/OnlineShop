namespace OnlineShop.Aggregator.DTOs;

public class ShoppingDTO
{
    public string UserName { get; set; } = string.Empty;
    public required BasketDTO BasketWithProduct { get; set; }
    public IEnumerable<OrderResponseDTO> Orders { get; set; } = [];
}
