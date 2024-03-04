using Hvalfangst.api.model;

namespace Hvalfangst.api.repository;

public interface IHeroRepository
{
    Task<IEnumerable<Hero>> GetAll();
    Task<Hero> GetById(int id);
    Task<Hero> GetByName(string name);
    Task Create(Hero hero);
    Task Update(Hero hero);
    Task Delete(int id);
}