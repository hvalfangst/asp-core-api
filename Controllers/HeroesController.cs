using Microsoft.AspNetCore.Mvc;
using Api.Configs;
using Api.Models;
using LoggerFactory = Api.Utils.LoggerFactory;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly AppConfig _config = new();
        private readonly List<Hero> _heroes = [];
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
}