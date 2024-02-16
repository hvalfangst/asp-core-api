namespace Api.Configs
{
    public class AppConfig
    {
        public string Difficulty { get; }
        public string Resolution { get; }
        public int Volume { get; }

        public AppConfig()
        {
            Difficulty = System.Environment.GetEnvironmentVariable("difficulty") ?? "Medium";
            Resolution = System.Environment.GetEnvironmentVariable("resolution") ?? "1920x1080";
            
            // If parsing volume as an integer fails, default to 50
            Volume = int.TryParse(System.Environment.GetEnvironmentVariable("volume"), out var parsedVolume) ? parsedVolume : 50;
        }
    }
}