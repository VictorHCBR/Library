using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Coupon> GetDiscount(string productId)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM DiscountCoupon WHERE ProductId = @ProductId", new { ProductId = productId });

        if (coupon == null)
            return new Coupon
            {
                ProductId = string.Empty,
                Description = string.Empty,
                Amount = 0,
            };

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var insertedRows = await connection.ExecuteAsync
            ("INSERT INTO DiscountCoupon (ProductId, Description, Amount) VALUES (@ProductId, @Description, @Amount)",
            new
            {
                coupon.ProductId,
                coupon.Description,
                coupon.Amount
            });

        if (insertedRows == 0)
            return false;

        return true;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var updatedRows = await connection.ExecuteAsync
            ("UPDATE DiscountCoupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount",
            new
            {
                coupon.ProductId,
                coupon.Description,
                coupon.Amount
            });

        return true;
    }

    public async Task<bool> DeleteDiscount(string productId)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var deletedRows = await connection.ExecuteAsync
            ("DELETE FROM DiscountCoupon WHERE ProductId = @ProductId", new { ProductId = productId });

        if (deletedRows == 0)
            return false;

        return true;
    }
}
