using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers;

[Route("api/productorders")]
[ApiController]
public class ProductOrdersController : ControllerBase
{
    private readonly DataBaseContext _db;

    public ProductOrdersController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.product_orders.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var productOrder = await _db.product_orders.FindAsync(id);
        return productOrder is not null ? Ok(productOrder) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductOrder productOrder)
    {
        _db.product_orders.Add(productOrder);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = productOrder.id }, productOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductOrder updateProductOrder)
    {
        var productOrder = await _db.product_orders.FindAsync(id);
        if (productOrder is null) return NotFound();

        productOrder.product_id = updateProductOrder.product_id;
        productOrder.order_id = updateProductOrder.order_id;
        productOrder.quantity = updateProductOrder.quantity;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var productOrder = await _db.product_orders.FindAsync(id);
        if (productOrder is null) return NotFound();

        _db.product_orders.Remove(productOrder);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
