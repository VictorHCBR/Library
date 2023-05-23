using Catalog.API.Data;
using Catalog.API.Repositories;

namespace Catalog.API.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureIoC(this IServiceCollection services)
    {
        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IBookRepository, BookRepository>();
    }
}
