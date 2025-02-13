using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly DataBaseContext _db;

    public RolesController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.roles.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var role = await _db.roles.FindAsync(id);
        return role is not null ? Ok(role) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Role role)
    {
        _db.roles.Add(role);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = role.id }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Role updateRole)
    {
        var role = await _db.roles.FindAsync(id);
        if (role is null) return NotFound();

        role.role_name = updateRole.role_name;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _db.roles.FindAsync(id);
        if (role is null) return NotFound();

        _db.roles.Remove(role);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
