using Discount.API.Repositories;
using Npgsql;

namespace Discount.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureIoC(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
    }

    public static void ConfigureMigration(this IServiceCollection services)
    {
    }

    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        var retryAvailability = retry.Value;
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            //var logger = services.GetRequiredService<ILogger>();

            try
            {
                using var connection = new NpgsqlConnection
                    (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                command.CommandText = "DROP TABLE IF EXISTS DiscountCoupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE DiscountCoupon(ID SERIAL PRIMARY KEY,
                                                                    ProductId VARCHAR(24) NOT NULL,
                                                                    Description TEXT,
                                                                    Amount INT)";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO DiscountCoupon(ProductId, Description, Amount)
                                        VALUES('646c2562a2cd4bbc1d0c4381', 'Comedy', 15)";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO DiscountCoupon(ProductId, Description, Amount)
                                        VALUES('646d12422b199df6eac1af50', 'Thriller', 12)";
                command.ExecuteNonQuery();

            }
            catch (NpgsqlException)
            {
                if (retryAvailability < 50)
                {
                    retryAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryAvailability);
                }
            }
        }

        return host;
    }
}
