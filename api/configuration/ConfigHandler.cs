using System.Text.Json;

namespace Hvalfangst.api.configuration;

public static class ConfigHandler
{

    public static LogConfig GetLogConfig()
    {
        
        // Read local configuration file if not on Kubernetes
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DEPLOYED_TO_K8S")))
        {
            const string fileName = @"env/log_config.json";
            var jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<LogConfig>(jsonString)!;
        }
        
        var path = Environment.GetEnvironmentVariable("LOG_PATH")
            ?? throw new InvalidOperationException("ENV 'LOG_PATH' is not set!");
        
        
        var prefix = Environment.GetEnvironmentVariable("LOG_PREFIX")
                   ?? throw new InvalidOperationException("ENV 'LOG_PREFIX' is not set!");

        return new LogConfig(path, prefix);
    }
    
    public static DatabaseConfig GetDbConfig()
    {
        
        // Read local configuration file if not on Kubernetes
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DEPLOYED_TO_K8S")))
        {
            const string fileName = @"env/db_config.json";
            var jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<DatabaseConfig>(jsonString)!;
        }
        
        var server = Environment.GetEnvironmentVariable("DB_SERVER")
                   ?? throw new InvalidOperationException("ENV 'DB_SERVER' is not set!");
        
        var port = Environment.GetEnvironmentVariable("DB_PORT")
                     ?? throw new InvalidOperationException("ENV 'DB_PORT' is not set!");
        
        var database  = Environment.GetEnvironmentVariable("DB_DATABASE")
                     ?? throw new InvalidOperationException("ENV 'DB_DATABASE' is not set!");
        
        var userId = Environment.GetEnvironmentVariable("DB_USERID")
                     ?? throw new InvalidOperationException("ENV 'DB_USERID' is not set!");
        
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD")
                     ?? throw new InvalidOperationException("ENV 'DB_PASSWORD' is not set!");

        return new DatabaseConfig(server, Convert.ToInt32(port), database, userId, password);
    }
    
}