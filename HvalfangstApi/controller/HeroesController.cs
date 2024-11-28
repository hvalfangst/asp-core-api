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

    [HttpGet]
    public async Task<IActionResult> List()
    {
        logger.LogInformation("Called endpoint GET heroes");
        var heroes = await heroService.GetAllHeroes();
        return Ok(heroes);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] HeroInputModel input)
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
}