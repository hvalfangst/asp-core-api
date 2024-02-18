using Api.Models;
using Api.Repositories;

namespace Api.Services;

public class HeroService(IHeroRepository heroRepository)
{
    private readonly IHeroRepository _heroRepository = heroRepository ?? throw new ArgumentNullException(nameof(heroRepository));

    public async Task<IEnumerable<Hero>> GetAllHeroes()
    {
        return await _heroRepository.GetAll();
    }

    public async Task<Hero> GetHeroById(int id)
    {
        return await _heroRepository.GetById(id);
    }

    public async Task<Hero> GetHeroByName(string name)
    {
        return await _heroRepository.GetByName(name);
    }

    public async Task CreateHero(Hero hero)
    {
        await _heroRepository.Create(hero);
    }

    public async Task UpdateHero(Hero hero)
    {
        await _heroRepository.Update(hero);
    }

    public async Task DeleteHero(int id)
    {
        await _heroRepository.Delete(id);
    }
}