using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly DataBaseContext _db;

    public AccountsController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.accounts.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var account = await _db.accounts.FindAsync(id);
        return account is not null ? Ok(account) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Account account)
    {
        _db.accounts.Add(account);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = account.id }, account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Account updateAccount)
    {
        var account = await _db.accounts.FindAsync(id);
        if (account is null) return NotFound();

        account.acc_login = updateAccount.acc_login;
        account.password = updateAccount.password;
        account.role_id = updateAccount.role_id;
        account.user_id = updateAccount.user_id;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _db.accounts.FindAsync(id);
        if (account is null) return NotFound();

        _db.accounts.Remove(account);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
