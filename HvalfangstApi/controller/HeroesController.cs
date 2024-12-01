using System.ComponentModel.DataAnnotations;
using HvalfangstApi.model;
using HvalfangstApi.model.request;
using HvalfangstApi.service;
using Microsoft.AspNetCore.Mvc;

namespace HvalfangstApi.controller;

[ApiController]
[Route("api/[controller]")]
public class HeroesController(HeroService heroService, ILogger<HeroesController> logger) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HeroInputModel input)
    {
        logger.LogInformation("Called endpoint POST heroes");

        try
        {
            input.Validate();

            var hero = new Hero
            {
                Name = input.Name,
                Class = input.Class,
                Level = input.Level,
                HitPoints = input.HitPoints,
                Damage = input.Damage,
                Attack = input.Attack,
                ArmorClass = input.ArmorClass
            };

            await heroService.CreateHero(hero);

            var message =
                $"Added new hero: Name {hero.Name}, Class: {hero.Class}, HitPoints: {hero.HitPoints}, ArmorClass: {hero.ArmorClass}, Attack: {hero.Attack}, Damage: {hero.Damage}";
            logger.LogInformation(message);
            
            return Ok(message);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning("Validation failed: {Errors}", ex.Message);
            return UnprocessableEntity(new { Errors = ex.Message.Split("; ") });
        }
    }
  
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] HeroInputModel input)
    {
        logger.LogInformation("Called endpoint PUT hero");

        try
        {
            input.Validate();

            var existingHero = await heroService.GetHeroById(id);

            existingHero.Name = input.Name;
            existingHero.Class = input.Class;
            existingHero.Level = input.Level;
            existingHero.HitPoints = input.HitPoints;
            existingHero.Damage = input.Damage;
            existingHero.Attack = input.Attack;
            existingHero.ArmorClass = input.ArmorClass;

            await heroService.UpdateHero(existingHero);

            var message =
                $"Updated hero: Name {existingHero.Name}, Class: {existingHero.Class}, HitPoints: {existingHero.HitPoints}, ArmorClass: {existingHero.ArmorClass}, Attack: {existingHero.Attack}, Damage: {existingHero.Damage}";
            logger.LogInformation(message);
            
            return Ok(message);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning("Validation failed: {Errors}", ex.Message);
            return UnprocessableEntity(new { Errors = ex.Message.Split("; ") });
        } catch (InvalidOperationException)
        {
            logger.LogWarning("Hero with ID [{Id}] not found", id);
            return NotFound(new { Message = $"Hero with ID [{id}] not found" });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> List()
    {
        logger.LogInformation("Called endpoint GET heroes");
        var heroes = await heroService.GetAllHeroes();
        return Ok(heroes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Called endpoint GET hero by ID");
        try
        {
            var hero = await heroService.GetHeroById(id);
            return Ok(hero);
        }
        catch (InvalidOperationException)
        {
            logger.LogWarning("Hero with ID [{Id}] not found", id);
            return NotFound(new { Message = $"Hero with ID [{id}] not found" });
        }
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        logger.LogInformation("Called endpoint GET hero by name");

        try
        {
            var hero = await heroService.GetHeroByName(name);
            return Ok(hero);
        }
        catch (InvalidOperationException)
        {
            logger.LogWarning("Hero with name [{name}] not found", name);
            return NotFound(new { Message = $"Hero with name [{name}] not found" });
        }
    }
    
    [HttpGet("compact/{id:int}")]
    public async Task<IActionResult> CompactGetById(int id)
    {
        logger.LogInformation("Called endpoint GET hero by ID");
        try
        {
            var hero = await heroService.GetHeroById(id);
            var result = $"{hero.Name}_{hero.Level}_{hero.Class}_{hero.HitPoints}_{hero.ArmorClass}_{hero.Attack}_{hero.Damage}";
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            logger.LogWarning("Hero with ID [{Id}] not found", id);
            return NotFound(new { Message = $"Hero with ID [{id}] not found" });
        }
    }

    [HttpGet("compact/name/{name}")]
    public async Task<IActionResult> CompactGetByName(string name)
    {
        logger.LogInformation("Called endpoint GET hero by name");

        try
        {
            var hero = await heroService.GetHeroByName(name);
            var result = $"{hero.Name}_{hero.Level}_{hero.Class}_{hero.HitPoints}_{hero.ArmorClass}_{hero.Attack}_{hero.Damage}";
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            logger.LogWarning("Hero with name [{name}] not found", name);
            return NotFound(new { Message = $"Hero with name [{name}] not found" });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("Called endpoint DELETE hero");

        try
        {
            await heroService.GetHeroById(id);
            await heroService.DeleteHero(id);
            var message = $"Deleted hero with ID [{id}]";
            logger.LogInformation(message);
            return Ok(new { Message = message });

        }
        catch (InvalidOperationException)
        {
            logger.LogWarning("Hero with ID [{Id}] not found", id);
            return NotFound(new { Message = $"Hero with ID [{id}] not found" });
        }
    }
}