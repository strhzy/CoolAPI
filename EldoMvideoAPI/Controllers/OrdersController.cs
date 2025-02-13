using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly DataBaseContext _db;

    public OrdersController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.orders.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _db.orders.FindAsync(id);
        return order is not null ? Ok(order) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        _db.orders.Add(order);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = order.id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order updateOrder)
    {
        var order = await _db.orders.FindAsync(id);
        if (order is null) return NotFound();

        order.account_id = updateOrder.account_id;
        order.delivery_id = updateOrder.delivery_id;
        order.order_date = updateOrder.order_date;
        order.total_sum = updateOrder.total_sum;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _db.orders.FindAsync(id);
        if (order is null) return NotFound();

        _db.orders.Remove(order);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
