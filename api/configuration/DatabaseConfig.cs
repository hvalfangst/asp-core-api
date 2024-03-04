namespace Hvalfangst.api.configuration;

public class DatabaseConfig(string server, int port, string schema, string userId, string password)
{
    public string Server { get; set; } = server;

    public int Port { get; set; } = port;
    public string Schema { get; set; } = schema;
    public string UserId { get; set; } = userId;
    public string Password { get; set; } = password;
}