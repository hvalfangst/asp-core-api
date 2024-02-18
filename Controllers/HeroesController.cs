using Microsoft.AspNetCore.Mvc;
using Api.Configs;
using Api.Models;
using Api.Services;
using LoggerFactory = Api.Utils.LoggerFactory;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController(HeroService heroService) : ControllerBase
    {
        private readonly AppConfig _config = new();
        private readonly ILogger<HeroesController> _logger = LoggerFactory.CreateLogger<HeroesController>();
        
        [HttpGet]
        public async Task<IActionResult> GetHeroes()
        {
            try
            {
                _logger.LogInformation("Called endpoint heroes");
                var heroes = await heroService.GetAllHeroes();
                return Ok(heroes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting heroes: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HeroInputModel input)
        {
            try
            {
                var hero = new Hero
                {
                    Name = input.Name,
                    Class = input.Class,
                    Damage = input.Damage,
                    Attack = input.Attack,
                    ArmorClass = input.ArmorClass
                };

                await heroService.CreateHero(hero);

                var message = $"Added new hero: {hero.Name}, Class: {hero.Class}, Damage: {hero.Damage}, Attack: {hero.Attack}, ArmorClass: {hero.ArmorClass}";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating hero: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
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
}