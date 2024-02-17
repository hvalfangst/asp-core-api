namespace Api.Utils;

public static class LoggerFactory
{
    private static readonly ILoggerFactory LoggerFactoryInstance = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        }));

    public static ILogger<T> CreateLogger<T>()
    {
        return LoggerFactoryInstance.CreateLogger<T>();
    }
}