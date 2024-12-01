using HvalfangstApi.repository;
using HvalfangstApi.controller;
using HvalfangstApi.model;
using HvalfangstApi.model.request;
using HvalfangstApi.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace HvalfangstTests
{
    public class HeroesControllerTests
    {
        [TestCase("TestHero1", "Warrior", 10, 100, 50, 20, 10)]
        [TestCase("TestHero2", "Mage", 8, 80, 60, 15, 10)]
        [TestCase("TestHero3", "Rogue", 9, 90, 55, 18, 10)]
        [TestCase("TestHero4", "Paladin", 11, 110, 45, 25, 10)]
        [TestCase("TestHero5", "Hunter", 9, 95, 52, 17, 10)]
        [TestCase("TestHero6", "Druid", 8, 85, 48, 22, 10)]
        [TestCase("TestHero7", "Shaman", 10, 105, 50, 19, 10)]
        [TestCase("TestHero8", "Priest", 7, 70, 40, 10, 10)]
        [TestCase("TestHero9", "Warlock", 7, 75, 65, 12, 10)]
        [TestCase("TestHero10", "Monk", 10, 100, 55, 20, 10)]
        public async Task Create_ShouldCreateHero_AndReturnOk(string name, string heroClass, int level, int hitPoints, int damage, int attack, int armorClass)
        {
            // Arrange
            var heroService = Substitute.For<HeroService>(Substitute.For<IHeroRepository>());
            var logger = Substitute.For<ILogger<HeroesController>>();
            var controller = new HeroesController(heroService, logger);

            var input = new HeroInputModel
            {
                Name = name,
                Class = heroClass,
                Level = level,
                HitPoints = hitPoints,
                Damage = damage,
                Attack = attack,
                ArmorClass = armorClass
            };

            // Act
            var result = await controller.Create(input);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value,
                Is.EqualTo($"Added new hero: Name {input.Name}, Class: {input.Class}, HitPoints: {input.HitPoints}, ArmorClass: {input.ArmorClass}, Attack: {input.Attack}, Damage: {input.Damage}"));        
        }
        
        [TestCase(null, "Warrior", 100, 50, 20,1  ,1)]
        [TestCase("TestHero", null, 100, 50, 20,1  ,1)]
        [TestCase("TestHero", "Warrior", -1, 50, 20,1  ,1)]
        [TestCase("TestHero", "Warrior", 100, -1, 20,1  ,1)]
        [TestCase("TestHero", "Warrior", 100, 50, -1,1  ,1)]

        public async Task Create_ShouldFailValidation_AndReturnBadRequest(string name, string heroClass, int level, int hitPoints, int damage, int attack, int armorClass)
        {
            // Arrange
            var heroService = Substitute.For<HeroService>(Substitute.For<IHeroRepository>());
            var logger = Substitute.For<ILogger<HeroesController>>();
            var controller = new HeroesController(heroService, logger);

            
            var input = new HeroInputModel
            {
                Name = name,
                Class = heroClass,
                Level = level,
                HitPoints = hitPoints,
                Damage = damage,
                Attack = attack,
                ArmorClass = armorClass
            };

            // Act
            var result = await controller.Create(input);

            // Assert
            Assert.That(result, Is.InstanceOf<UnprocessableEntityObjectResult>());

        }

        [TestCase("TestHero1", "Warrior", 10, 100, 50, 20, 10)]
        [TestCase("TestHero2", "Mage", 8, 80, 60, 15, 10)]
        [TestCase("TestHero3", "Rogue", 9, 90, 55, 18, 10)]
        [TestCase("TestHero4", "Paladin", 11, 110, 45, 25, 10)]
        [TestCase("TestHero5", "Hunter", 9, 95, 52, 17, 10)]
        [TestCase("TestHero6", "Druid", 8, 85, 48, 22, 10)]
        [TestCase("TestHero7", "Shaman", 10, 105, 50, 19, 10)]
        [TestCase("TestHero8", "Priest", 7, 70, 40, 10, 10)]
        [TestCase("TestHero9", "Warlock", 7, 75, 65, 12, 10)]
        [TestCase("TestHero10", "Monk", 10, 100, 55, 20, 10)]
        public async Task Update_ShouldUpdateHero_AndReturnOk(string name, string heroClass, int level, int hitPoints, int damage, int attack, int armorClass)
        {
            // Arrange
            var heroService = Substitute.For<HeroService>(Substitute.For<IHeroRepository>());
            var logger = Substitute.For<ILogger<HeroesController>>();
            var controller = new HeroesController(heroService, logger);

            var input = new HeroInputModel
            {
                Name = name,
                Class = heroClass,
                Level = level,
                HitPoints = hitPoints,
                Damage = damage,
                Attack = attack,
                ArmorClass = armorClass
            };

            var existingHero = new Hero
            {
                Name = "OldName",
                Class = "OldClass",
                Level = 1,
                HitPoints = 1,
                Damage = 1,
                Attack = 1,
                ArmorClass = 1
            };

            heroService.GetHeroById(1).Returns(existingHero);

            // Act
            var result = await controller.Update(1, input);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value,
                Is.EqualTo($"Updated hero: Name {input.Name}, Class: {input.Class}, HitPoints: {input.HitPoints}, ArmorClass: {input.ArmorClass}, Attack: {input.Attack}, Damage: {input.Damage}"));
        }
        
        [TestCase(null, "Warrior", 100, 50, 20,1  ,1)]
        [TestCase("TestHero", null, 100, 50, 20,1  ,1)]
        [TestCase("TestHero", "Warrior", -1, 50, 20,1  ,1)]
        [TestCase("TestHero", "Warrior", 100, -1, 20,1  ,1)]
        [TestCase("TestHero", "Warrior", 100, 50, -1,1  ,1)]

        public async Task Update_ShouldFailValidation_AndReturnBadRequest(string name, string heroClass, int level, int hitPoints, int damage, int attack, int armorClass)
        {
            // Arrange
            var heroService = Substitute.For<HeroService>(Substitute.For<IHeroRepository>());
            var logger = Substitute.For<ILogger<HeroesController>>();
            var controller = new HeroesController(heroService, logger);

            var input = new HeroInputModel
            {
                Name = name,
                Class = heroClass,
                Level = level,
                HitPoints = hitPoints,
                Damage = damage,
                Attack = attack,
                ArmorClass = armorClass
            };

            var existingHero = new Hero
            {
                Name = "OldName",
                Class = "OldClass",
                Level = 1,
                HitPoints = 1,
                Damage = 1,
                Attack = 1,
                ArmorClass = 1
            };

            heroService.GetHeroById(1).Returns(existingHero);

            // Act
            var result = await controller.Update(1, input);

            // Assert
            Assert.That(result, Is.InstanceOf<UnprocessableEntityObjectResult>());
        }
       
    }
}