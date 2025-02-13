using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveriesController : ControllerBase
{
    private readonly DataBaseContext _db;

    public DeliveriesController(DataBaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.deliveries.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var delivery = await _db.deliveries.FindAsync(id);
        return delivery is not null ? Ok(delivery) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Delivery delivery)
    {
        _db.deliveries.Add(delivery);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = delivery.id }, delivery);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Delivery updateDelivery)
    {
        var delivery = await _db.deliveries.FindAsync(id);
        if (delivery is null) return NotFound();

        delivery.address = updateDelivery.address;
        delivery.delivery_date = updateDelivery.delivery_date;
        delivery.delivery_time = updateDelivery.delivery_time;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var delivery = await _db.deliveries.FindAsync(id);
        if (delivery is null) return NotFound();

        _db.deliveries.Remove(delivery);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
