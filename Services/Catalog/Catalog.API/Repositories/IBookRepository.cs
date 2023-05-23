using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooks();
    Task<Book> GetBookById(string id);
    Task<IEnumerable<Book>> GetBookByTitle(string title);
    Task<IEnumerable<Book>> GetBooksByCategory(string category);
    Task RegisterBook(Book book);
    Task<bool> UpdateBook(Book book);
    Task<bool> DeleteBook(string id);
}