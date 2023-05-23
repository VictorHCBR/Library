using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(BookCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BookCart>> GetBasket(string username)
    {
        var basket = await _repository.GetBasket(username);

        return Ok(basket ?? new BookCart(username));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BookCart>> UpdateBasket([FromBody] BookCart bookCart)
    {
        return Ok(await _repository.UpdateBasket(bookCart));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket(string username)
    {
        await _repository.DeleteBasket(username);
        return Ok();
    }
}
