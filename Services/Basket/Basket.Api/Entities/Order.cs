namespace Basket.Api.Entities;

public class Order(string userName)
{
    public string UserName { get; set; } = userName;
    public List<OrderItem> Items { get; set; } = [];
    public decimal TotalPrice 
    { 
        get 
        {
            return Items.Sum(x => x.Quantity * x.Price);
        } 
    }
}
