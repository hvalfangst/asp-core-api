namespace Hvalfangst.api.util.logging;

public class FileLogger(string logPath) : ILogger
{

    public void Log<TState>(LogLevel level, EventId id, TState state, Exception? exception, Func<TState, Exception, string> formatter)
    {
        using var writer = new StreamWriter(logPath, append: true);
        var message = formatter(state, exception!);
        var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

        if (exception != null)
        {
            logEntry += Environment.NewLine + exception.Message;
        }
        
        writer.WriteLine(logEntry);
    }
    
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel level)
    {
        return true;
    }
    
}