using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IBookRepository bookRepository, ILogger<CatalogController> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bookRepository.GetBooks();
        if (!books.Any())
            return NotFound("No books found on database.");

        return Ok(books);
    }

    [HttpGet("{id:length(24)}", Name = "GetBookById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Book>> GetBookById(string id)
    {
        var book = await _bookRepository.GetBookById(id);
        if (book == null)
        {
            _logger.LogError("No book with id '{0}' was found!", id);
            return NotFound();
        }

        return Ok(book);
    }

    [HttpGet]
    [Route("[action]/{category}", Name = "GetBooksByCategory")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooksByCategory(string category)
    {
        var books = await _bookRepository.GetBooksByCategory(category);
        if (!books.Any())
        {
            _logger.LogError($"No books of category '{category}' not found");
            return NotFound();
        }
        return Ok(books);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Book>> RegisterBook([FromBody] Book book)
    {
        await _bookRepository.RegisterBook(book);
        return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBook([FromBody] Book book)
    {
        return Ok(await _bookRepository.UpdateBook(book));
    }

    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
        return Ok(await _bookRepository.DeleteBook(id));
    }
}
