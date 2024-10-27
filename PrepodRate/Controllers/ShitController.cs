using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using PrepodRate.Data;

namespace PrepodRate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    
    public ShitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{hyi:int}")]
    public string AcceptShit(int hyi, [FromQuery] string unit = "cm")
    {
        return $"Ваш хуй: {hyi}{unit}\n";
    }

    // [HttpPost]
    // public async Task<HyiModel> PostHyi([FromBody] HyiModel hyiModel)
    // {
    //     
    // }

    [HttpGet("etalon")]
    public HyiModel GetEtalon()
    {
        return new HyiModel { Name = "Валерий", Length = 100000 };
    }
}

public class HyiModel
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("length")]
    public int Length { get; set; }
}
