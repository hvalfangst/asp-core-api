using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Api.Configs;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly AppConfig _config = new();
        private readonly List<Hero> _heroes = [];

        private static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "HH:mm:ss ";
            }));
    
        private readonly ILogger<HeroesController> _logger = LoggerFactory.CreateLogger<HeroesController>();
        
        [HttpGet]
        public IActionResult GetHeroes()
        {
            _logger.LogInformation("Called endpoint heroes");
            return Ok(_heroes);
        }

        [HttpPost]
        public IActionResult Post([FromBody] HeroInputModel input)
        {
            var hero = new Hero
            {
                Name = input.Name,
                Class = input.Class,
                Damage = input.Damage,
                Attack = input.Attack,
                ArmorClass = input.ArmorClass
            };

            _heroes.Add(hero);
            var message = $"Added new hero: {hero.Name}, Class: {hero.Class}, Damage: {hero.Damage}, Attack: {hero.Attack}, ArmorClass: {hero.ArmorClass}";
            _logger.LogInformation(message);
            return Ok(message);
        }
        
        [HttpGet("config")]
        public IActionResult GetAppConfig()
        {
            _logger.LogInformation("Called endpoint config");
            return Ok(new
            {
                _config.Difficulty,
                _config.Resolution,
                _config.Volume
            });
        } 
    }

    public class Hero
    {
        public string? Name { get; init; }
        public string? Class { get; init; }
        public int Damage { get; init; }
        public int Attack { get; init; }
        public int ArmorClass { get; init; }
    }

    public class HeroInputModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Class is required.")]
        [StringLength(20, ErrorMessage = "Class cannot exceed 20 characters.")]
        public string? Class { get; set; }

        [Required(ErrorMessage = "Damage is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Damage must be non-negative.")]
        public int Damage { get; set; }

        [Required(ErrorMessage = "Attack is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Attack must be non-negative.")]
        public int Attack { get; set; }

        [Required(ErrorMessage = "ArmorClass is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "ArmorClass must be non-negative.")]
        public int ArmorClass { get; set; }
    }
}