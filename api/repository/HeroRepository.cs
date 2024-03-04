using Dapper;
using Hvalfangst.api.configuration;
using Hvalfangst.api.model;

namespace Hvalfangst.api.repository;

public class HeroRepository(DataContext context) : IHeroRepository
{
    public async Task<IEnumerable<Hero>> GetAll()
        {
            using var connection = context.CreateConnection();
            var sql = "SELECT * FROM heroes";
            return await connection.QueryAsync<Hero>(sql);
        }

        public async Task<Hero> GetById(int id)
        {
            using var connection = context.CreateConnection();
            var sql = "SELECT * FROM heroes WHERE Id = @id";
            return await connection.QuerySingleOrDefaultAsync<Hero>(sql, new { id }) ?? throw new InvalidOperationException();
        }

        public async Task<Hero> GetByName(string name)
        {
            using var connection = context.CreateConnection();
            var sql = "SELECT * FROM heroes WHERE Name = @name";
            return await connection.QuerySingleOrDefaultAsync<Hero>(sql, new { name }) ?? throw new InvalidOperationException();
        }

        public async Task Create(Hero hero)
        {
            using var connection = context.CreateConnection();
            var sql = @"
                INSERT INTO heroes (Name, Class, Level, HitPoints, Damage, Attack, ArmorClass)
                VALUES (@Name, @Class, @Level, @HitPoints, @Damage, @Attack, @ArmorClass)";
            await connection.ExecuteAsync(sql, hero);
        }

        public async Task Update(Hero hero)
        {
            using var connection = context.CreateConnection();
            var sql = @"
                UPDATE heroes 
                SET Name = @Name,
                    Class = @Class,
                    Level = @Level,
                    HitPoints = @HitPoints,
                    Damage = @Damage,
                    Attack = @Attack,
                    ArmorClass = @ArmorClass
                WHERE Id = @Id";
            await connection.ExecuteAsync(sql, hero);
        }

        public async Task Delete(int id)
        {
            using var connection = context.CreateConnection();
            var sql = "DELETE FROM heroes WHERE Id = @id";
            await connection.ExecuteAsync(sql, new { id });
        }
    
}