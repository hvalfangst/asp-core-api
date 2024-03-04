namespace Hvalfangst.api.configuration
{
    public class LogConfig(string logPath, string logPrefix)
    {
        public string LogPath { get;  } = logPath;

        public string LogPrefix { get;  } = logPrefix;
    }
}