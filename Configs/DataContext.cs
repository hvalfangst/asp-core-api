using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Api.Configs;

public class DataContext(IOptions<DatabaseConfig> dbSettings)
{
    private readonly DatabaseConfig _dbSettings = dbSettings.Value;

    public IDbConnection CreateConnection()
    {
        var connectionString = $"Host={_dbSettings.Server}; Port={_dbSettings.Port}; Database={_dbSettings.Database}; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
        return new NpgsqlConnection(connectionString);
    }
}