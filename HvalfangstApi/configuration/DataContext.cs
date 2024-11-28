using System.Data;
using Npgsql;

namespace HvalfangstApi.configuration;

public class DataContext
{
    private readonly DatabaseConfig _config = ConfigHandler.GetDbConfig();

    public IDbConnection CreateConnection()
    {
        var connectionString = $"Host={_config.Server}; " +
                               $"Port={_config.Port}; " +
                               $"Database={_config.Schema}; " +
                               $"Username={_config.UserId}; " +
                               $"Password={_config.Password};";
        return new NpgsqlConnection(connectionString);
    }
}