namespace Api.Utils;

public class FileLoggerProvider(string path) : ILoggerProvider
{

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path);
    }
    
    public void Dispose() { }
    
}