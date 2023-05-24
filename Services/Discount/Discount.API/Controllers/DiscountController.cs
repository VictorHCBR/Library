using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _couponRepository;

    public DiscountController(IDiscountRepository repository)
    {
        _couponRepository = repository;
    }

    [HttpGet("{productId}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> GetDiscount(string productId)
    {
        var couponDiscount = await _couponRepository.GetDiscount(productId);
        return Ok(couponDiscount);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        await _couponRepository.CreateDiscount(coupon);
        return CreatedAtRoute("", new { productId = coupon.Id }, coupon);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> UpdateCoupon([FromBody] Coupon coupon)
    {
        return Ok(await _couponRepository.UpdateDiscount(coupon));
    }

    [HttpDelete("{productId}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCoupon(string productId)
    {
        return Ok(await _couponRepository.DeleteDiscount(productId));
    }
}
