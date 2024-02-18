namespace Api.Configs;

public class DatabaseConfig
{
    public string Server { get; set; }
    
    public int Port { get; set; }
    public string Database { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }

    // Parameterless constructor
    public DatabaseConfig()
    {
    }
}