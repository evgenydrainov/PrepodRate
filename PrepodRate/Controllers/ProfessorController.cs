using Microsoft.AspNetCore.Mvc;
using PrepodRate.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace PrepodRate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
	private readonly ApplicationDbContext _db;

	public ProfessorController(ApplicationDbContext db)
	{
		_db = db;
	}

	[HttpGet("all")]
	public async Task<IEnumerable<Professor>> GetAllProfessorsAsync()
	{
		return await _db.Professors.ToListAsync();
	}

	[HttpGet("{id:int?}")]
	public async Task<Professor> Get(int? id = null)
	{
		var prof = await _db.Professors.FirstOrDefaultAsync(x => x.Id == (id ?? 1));
		return prof;
	}

	[HttpPost]
	public async Task<IActionResult> AddProfessor([FromBody] Professor prof)
	{
		await _db.Professors.AddAsync(prof);
		await _db.SaveChangesAsync();
		return NoContent();
	}

	[HttpGet("test")]
	public string GetTest()
	{
		return "Test";
	}
}

public class Professor
{
	[JsonPropertyName("Id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[JsonPropertyName("Name")]
	public string Name { get; set; }

	[JsonPropertyName("Surname")]
	public string Surname { get; set; }

	[JsonPropertyName("Patronymic")]
	public string Patronymic { get; set; }

	[JsonPropertyName("University")]
	public string University { get; set; }
}
