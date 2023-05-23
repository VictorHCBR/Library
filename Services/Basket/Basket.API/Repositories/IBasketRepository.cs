using Basket.API.Entities;

namespace Basket.API.Repositories;

public interface IBasketRepository
{
    Task<BookCart> GetBasket(string userName);
    Task<BookCart> UpdateBasket(BookCart bookCart);
    Task DeleteBasket(string userName);
}
