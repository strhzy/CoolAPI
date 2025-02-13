using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly DataBaseContext _db;

    public CategoriesController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.categories.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _db.categories.FindAsync(id);
        return category is not null ? Ok(category) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        _db.categories.Add(category);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = category.id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Category updateCategory)
    {
        var category = await _db.categories.FindAsync(id);
        if (category is null) return NotFound();

        category.category = updateCategory.category;
        category.descript = updateCategory.descript;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _db.categories.FindAsync(id);
        if (category is null) return NotFound();

        _db.categories.Remove(category);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
