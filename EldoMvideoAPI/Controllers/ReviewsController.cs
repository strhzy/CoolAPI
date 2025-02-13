using EldoMvideoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldoMvideoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly DataBaseContext _db;

        public ReviewsController(DataBaseContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _db.reviews.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _db.reviews.FindAsync(id);
            return review is not null ? Ok(review) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            _db.reviews.Add(review);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = review.id }, review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Review updateReview)
        {
            var review = await _db.reviews.FindAsync(id);
            if (review is null) return NotFound();

            review.user_id = updateReview.user_id;
            review.product_id = updateReview.product_id;
            review.rating = updateReview.rating;
            review.review = updateReview.review;
            review.date = updateReview.date;
        
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _db.reviews.FindAsync(id);
            if (review is null) return NotFound();

            _db.reviews.Remove(review);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
