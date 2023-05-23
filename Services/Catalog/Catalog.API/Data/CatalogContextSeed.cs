using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Book> bookCollection)
    {
        bool existProduct = bookCollection.Find(b => true).Any();

        if (!existProduct)
        {
            bookCollection.InsertManyAsync(GetSeedBooks());
        }
    }

    private static IEnumerable<Book> GetSeedBooks()
    {
        return new List<Book>()
        {
            new Book()
            {
                Id = "646c2562a2cd4bbc1d0c4381",
                Title = "First Book of a Successful Vendor",
                Author = "Successful Dude",
                Publisher = "Successful Publisher",
                Year = 1992,
                ISBN = "9781234567897",
                Language = "English",
                Category = "Comedy"
            }
        };
    }
}
