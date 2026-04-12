using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.IService;
using RestApi.Data;



namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CreditDbContext _context;

        public WeatherForecastController( ILogger<WeatherForecastController> logger, CreditDbContext context) { 
           
            _logger = logger;
            _context = context;
        }
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Weather data requested at {Time}", DateTime.Now);
            try
            {


                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating weather data at {Time}", DateTime.Now);
                throw;
            }    
        }

        [HttpGet("GetBasicMembers")]
        public async Task<ActionResult<IEnumerable<basic_member>>> GetBasicMembers()
        {
            _logger.LogInformation("GetBasicMembers requested at {Time}", DateTime.Now);
            try
            {
                IQueryable<basic_member> query = _context.basic_members;
                query = query.Take(5);

                var basicMembers = await query.ToListAsync();
                _logger.LogInformation("Successfully retrieved {Count} basic members", basicMembers.Count);
                return Ok(basicMembers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving basic members at {Time}", DateTime.Now);
                return StatusCode(500, new { message = "Error retrieving basic members", error = ex.Message });
            }
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<category>>> GetCategories()
        {
            _logger.LogInformation("GetCategories requested at {Time}", DateTime.Now);
            try
            {
                IQueryable<category> query = _context.categories;

                var categories = await query.ToListAsync();
                _logger.LogInformation("Successfully retrieved {Count} categories", categories.Count);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving categories at {Time}", DateTime.Now);
                return StatusCode(500, new { message = "Error retrieving categories", error = ex.Message });
            }
        }

        [HttpGet("GetCorporations")]
        public async Task<ActionResult<IEnumerable<corporation>>> GetCorporations()
        {
            _logger.LogInformation("GetCorporations requested at {Time}", DateTime.Now);
            try
            {
                IQueryable<corporation> query = _context.corporations;

                var corporations = await query.ToListAsync();
                _logger.LogInformation("Successfully retrieved {Count} corporations", corporations.Count);
                return Ok(corporations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving corporations at {Time}", DateTime.Now);
                return StatusCode(500, new { message = "Error retrieving corporations", error = ex.Message });
            }
        }
    }
}
