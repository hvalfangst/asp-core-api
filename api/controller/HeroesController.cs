using Hvalfangst.api.model;
using Hvalfangst.api.model.request;
using Hvalfangst.api.service;
using Microsoft.AspNetCore.Mvc;

namespace Hvalfangst.api.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController(HeroService heroService, ILogger<HeroesController> logger) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            logger.LogInformation("Called endpoint GET heroes");
            
            try
            {
               
                var heroes = await heroService.GetAllHeroes();
                return Ok(heroes);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error listing heroes: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HeroInputModel input)
        {
            logger.LogInformation("Called endpoint POST heroes");
            
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
                logger.LogInformation(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error creating hero: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}