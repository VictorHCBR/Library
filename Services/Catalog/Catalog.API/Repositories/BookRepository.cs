using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ICatalogContext _context;

    public BookRepository(ICatalogContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Book>> GetBooks()
    {
        return await _context.Books.Find(p => true).ToListAsync();
    }

    public async Task<Book> GetBookById(string id)
    {
        return await _context.Books.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Book>> GetBookByTitle(string title)
    {
        FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(p => p.Title, title);

        return await _context.Books.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByCategory(string category)
    {
        FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(b => b.Category, category);

        return await _context.Books.Find(filter).ToListAsync();
    }

    public async Task RegisterBook(Book book)
    {
        await _context.Books.InsertOneAsync(book);
    }

    public async Task<bool> UpdateBook(Book book)
    {
        var updatedBook = await _context.Books
                                .ReplaceOneAsync(filter: s => s.Id == book.Id, replacement: book);

        return updatedBook.IsAcknowledged
            && updatedBook.ModifiedCount > 0;
    }

    public async Task<bool> DeleteBook(string id)
    {
        FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(b => b.Id, id);

        DeleteResult deleteResult = await _context
                                          .Books
                                          .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged &&
               deleteResult.DeletedCount > 0;
    }
}
