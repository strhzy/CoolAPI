using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DataBaseContext _db;

    public UsersController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.users.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _db.users.FindAsync(id);
        return user is not null ? Ok(user) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        _db.users.Add(user);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = user.id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User updateUser)
    {
        var user = await _db.users.FindAsync(id);
        if (user is null) return NotFound();

        user.first_name = updateUser.first_name;
        user.middle_name = updateUser.middle_name;
        user.last_name = updateUser.last_name;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.users.FindAsync(id);
        if (user is null) return NotFound();

        _db.users.Remove(user);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
