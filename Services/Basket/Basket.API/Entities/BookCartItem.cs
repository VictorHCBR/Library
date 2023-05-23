namespace Basket.API.Entities;

public class BookCartItem
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string BookId { get; set; }
    public string BookName { get; set; }
}