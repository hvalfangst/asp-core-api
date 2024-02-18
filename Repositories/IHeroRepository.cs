
using Api.Models;

namespace Api.Repositories;

public interface IHeroRepository
{
    Task<IEnumerable<Hero>> GetAll();
    Task<Hero> GetById(int id);
    Task<Hero> GetByName(string name);
    Task Create(Hero hero);
    Task Update(Hero hero);
    Task Delete(int id);
}