namespace Basket.API.Entities;

public class BookCart
{
    public string UserName { get; set; }
    public List<BookCartItem> Items { get; set; } = new List<BookCartItem>();

    public BookCart()
    {

    }

    public BookCart(string userName)
    {
        UserName = userName;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var item in Items)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
    }
}
