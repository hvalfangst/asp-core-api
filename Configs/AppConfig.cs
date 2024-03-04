namespace Api.Configs
{
    public class AppConfig
    {
        public string Difficulty { get; }
        public string Resolution { get; }
        public int Volume { get; }
        public string DatabaseUrl { get; }
        
        public string LogPath { get;  }

        public AppConfig()
        {
            Difficulty = Environment.GetEnvironmentVariable("difficulty") ?? "Medium";
            Resolution = Environment.GetEnvironmentVariable("resolution") ?? "1920x1080";
            
            // If parsing volume as an integer fails, default to 50
            Volume = int.TryParse(System.Environment.GetEnvironmentVariable("volume"), out var parsedVolume) ? parsedVolume : 50;

            // Set the DatabaseUrl property, defaulting to your defined connection string
            DatabaseUrl = System.Environment.GetEnvironmentVariable("DATABASE_URL") ?? "Host=localhost;Port=5434;Username=MP77;Password=IDecreeAndDeclareWarOnShitePerformance;Database=heroes_db";
            LogPath = "logs/api.log";
        }
        
    }
}