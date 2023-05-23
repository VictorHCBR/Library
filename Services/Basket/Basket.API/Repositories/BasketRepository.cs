using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<BookCart> GetBasket(string userName)
    {
        var basket = await _cache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonConvert.DeserializeObject<BookCart>(basket);
    }

    public async Task<BookCart> UpdateBasket(BookCart basket)
    {
        await _cache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

        return await GetBasket(basket.UserName);
    }
    public async Task DeleteBasket(string userName)
    {
        await _cache.RemoveAsync(userName);
    }
}
