using EldoMvideoAPI.Models;

namespace EldoMvideoAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly DataBaseContext _db;

    public ProductsController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.products.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _db.products.FindAsync(id);
        return product is not null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        _db.products.Add(product);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product updateProduct)
    {
        var product = await _db.products.FindAsync(id);
        if (product is null) return NotFound();

        product.product_name = updateProduct.product_name;
        product.category_id = updateProduct.category_id;
        product.pic_link = updateProduct.pic_link;
        product.price = updateProduct.price;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _db.products.FindAsync(id);
        if (product is null) return NotFound();

        _db.products.Remove(product);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
