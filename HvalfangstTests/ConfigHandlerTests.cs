using HvalfangstApi.configuration;

namespace HvalfangstApi.Tests
{
    public class ConfigHandlerTests
    {
        // [SetUp]
        // public void SetUp()
        // {
        //     // Clear environment variables before each test
        //     Environment.SetEnvironmentVariable("DEPLOYED_TO_K8S", null);
        //     Environment.SetEnvironmentVariable("LOG_PATH", null);
        //     Environment.SetEnvironmentVariable("LOG_PREFIX", null);
        //     Environment.SetEnvironmentVariable("DB_SERVER", null);
        //     Environment.SetEnvironmentVariable("DB_PORT", null);
        //     Environment.SetEnvironmentVariable("DB_DATABASE", null);
        //     Environment.SetEnvironmentVariable("DB_USERID", null);
        //     Environment.SetEnvironmentVariable("DB_PASSWORD", null);
        // }
        //
        //
        // [Test]
        // public void GetLogConfig_ShouldReturnEnvConfig_WhenOnKubernetes()
        // {
        //     // Arrange
        //     Environment.SetEnvironmentVariable("DEPLOYED_TO_K8S", "true");
        //     Environment.SetEnvironmentVariable("LOG_PATH", "/var/logs");
        //     Environment.SetEnvironmentVariable("LOG_PREFIX", "app");
        //
        //     // Act
        //     var result = ConfigHandler.GetLogConfig();
        //
        //     // Assert
        //     Assert.AreEqual("/var/logs", result.LogPath);
        //     Assert.AreEqual("app", result.LogPrefix);
        // }
        //
        // [Test]
        // public void GetDbConfig_ShouldReturnEnvConfig_WhenOnKubernetes()
        // {
        //     // Arrange
        //     Environment.SetEnvironmentVariable("DEPLOYED_TO_K8S", "true");
        //     Environment.SetEnvironmentVariable("DB_SERVER", "localhost");
        //     Environment.SetEnvironmentVariable("DB_PORT", "5432");
        //     Environment.SetEnvironmentVariable("DB_DATABASE", "testdb");
        //     Environment.SetEnvironmentVariable("DB_USERID", "user");
        //     Environment.SetEnvironmentVariable("DB_PASSWORD", "pass");
        //
        //     // Act
        //     var result = ConfigHandler.GetDbConfig();
        //
        //     // Assert
        //     Assert.AreEqual("localhost", result.Server);
        //     Assert.AreEqual(5432, result.Port);
        // }
    }
}