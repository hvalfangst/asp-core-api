using System.Net;
using System.Text;
using HvalfangstApi;
using HvalfangstApi.repository;
using HvalfangstApi.service;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;

namespace HvalfangstTests;

public class HeroesControllerIntegrationTests
{
    private HttpClient _client;

    [SetUp]
    public void SetUp()
    {
        // Mocking the HeroService
        var heroService = Substitute.For<HeroService>(Substitute.For<IHeroRepository>());
        var heroRepository = Substitute.For<IHeroRepository>();
        
        // Creating a WebApplicationFactory with the Program class
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(heroService);
                    services.AddSingleton(heroRepository);
                });
            });
        _client = factory.CreateClient();
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
    public async Task Create_ShouldCreateHero_AndReturnOk(string name, string heroClass, int level, int hitPoints, int damage, int attack, int armorClass)
    {
        // Arrange
        var heroInput = new
        {
            Name = name,
            Class = heroClass,
            Level = level,
            HitPoints = hitPoints,
            Damage = damage,
            Attack = attack,
            ArmorClass = armorClass
        };
        var content = new StringContent(JsonConvert.SerializeObject(heroInput), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/heroes", content);

         }
    
    [TestCase(null, "Warrior", 100, 50, 20,1  ,1)]
    [TestCase("TestHero", null, 100, 50, 20,1  ,1)]
    [TestCase("TestHero", "Warrior", -1, 50, 20,1  ,1)]
    [TestCase("TestHero", "Warrior", 100, -1, 20,1  ,1)]
    [TestCase("TestHero", "Warrior", 100, 50, -1,1  ,1)]

    public async Task Create_ShouldFailValidation_AndReturnBadRequest(string name, string heroClass, int level, int hitPoints, int damage, int attack, int armorClass)
    {
        
        // Arrange
        var heroInput = new
        {
            Name = name,
            Class = heroClass,
            Level = level,
            HitPoints = hitPoints,
            Damage = damage,
            Attack = attack,
            ArmorClass = armorClass
        };

        var content = new StringContent(JsonConvert.SerializeObject(heroInput), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/heroes", content);
        
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    
        /*[TestCase("TestHero1", "Warrior", 10, 100, 50, 20, 10)]
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
            var existingHero = new
            {
                Name = "OldName",
                Class = "OldClass",
                Level = 1,
                HitPoints = 1,
                Damage = 1,
                Attack = 1,
                ArmorClass = 1
            };
            
            var contentFirst = new StringContent(JsonConvert.SerializeObject(existingHero), Encoding.UTF8, "application/json");
            var responseFirst = await _client.PostAsync("/api/heroes", contentFirst);
            
            // Assert
            responseFirst.EnsureSuccessStatusCode();
            var responseString = await responseFirst.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.EqualTo($"Added new hero: Name {existingHero.Name}, Class: {existingHero.Class}, HitPoints: {existingHero.HitPoints}, ArmorClass: {existingHero.ArmorClass}, Attack: {existingHero.Attack}, Damage: {existingHero.Damage}"));
            
            var input = new
            {
                Name = name,
                Class = heroClass,
                Level = level,
                HitPoints = hitPoints,
                Damage = damage,
                Attack = attack,
                ArmorClass = armorClass
            };
            
            var contentSecond = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            var responseSecond = await _client.PutAsync("/api/heroes/1", contentSecond);
            
            // Assert
            responseSecond.EnsureSuccessStatusCode();
            var responseStringSecond = await responseSecond.Content.ReadAsStringAsync();
            Assert.That(responseStringSecond, Is.EqualTo($"Updated hero with id 1"));
        }*/
        
    
    [Test]
    public async Task Calling_InvalidURI_ShouldReturn404()
    {
        
        // Act
        var response = await _client.GetAsync("PANDA_POWER");
        
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}