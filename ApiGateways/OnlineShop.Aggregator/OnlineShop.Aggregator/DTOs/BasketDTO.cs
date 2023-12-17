namespace OnlineShop.Aggregator.DTOs;

public class BasketDTO
{
    public string UserName { get; set; } = string.Empty;
    public List<BasketItemExtendedDTO> Items { get; set; } = [];
    public decimal TotalPrice { get; set; }
}
