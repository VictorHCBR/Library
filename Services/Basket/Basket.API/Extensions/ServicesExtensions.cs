using Basket.API.Repositories;

namespace Basket.API.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });
    }

    public static void ConfigureIoC(this IServiceCollection services)
    {
        services.AddScoped<IBasketRepository, BasketRepository>();
    }
}
