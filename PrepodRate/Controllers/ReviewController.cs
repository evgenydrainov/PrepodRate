using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrepodRate.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrepodRate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
	private readonly ApplicationDbContext _db;

	public ReviewController(ApplicationDbContext db)
	{
		_db = db;
	}

	[HttpGet("all")]
	public async Task<IEnumerable<Review>> GetAllReviewsAsync()
	{
		return await _db.Reviews.ToListAsync();
	}

	[HttpGet("{id:int?}")]
	public async Task<Review> Get(int? id = null)
	{
		return await _db.Reviews.FirstOrDefaultAsync(x => x.Id == (id ?? 1));
	}

	[HttpGet("professor/{professorId:int}")]
	public async Task<IEnumerable<Review>> GetByProfessorId(int professorId)
	{
		List<Review> result = new List<Review>();
		await _db.Reviews.ForEachAsync(review =>
		{
			if (review.ProfessorId == professorId) {
				result.Add(review);
			}
		});
		return result;
	}

	[HttpPost]
	public async Task<IActionResult> AddReview([FromBody] Review review)
	{
		await _db.Reviews.AddAsync(review);
		await _db.SaveChangesAsync();
		return NoContent();
	}
}

public class Review
{
	[JsonPropertyName("Id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[JsonPropertyName("ProfessorId")]
	public int ProfessorId { get; set; }

	[JsonPropertyName("Rating")]
	public float Rating { get; set; }

	[JsonPropertyName("Body")]
	public string Body { get; set; }
}
