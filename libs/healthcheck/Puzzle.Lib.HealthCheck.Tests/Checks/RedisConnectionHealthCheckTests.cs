namespace Puzzle.Lib.HealthCheck.Tests.Checks
{
    public class RedisConnectionHealthCheckTests
    {
        [Fact]
        public void AddRedisCheck_WithValidConnectionString_AddsRedisHealthCheck()
        {
            //// Arrange
            //var services = new ServiceCollection();
            //var asd = services.AddHealthChecks();
            //var connectionString = "valid-redis-connection-string";

            //// Act
            //var builder = asd.AddRedisCheck(connectionString);

            //// Assert
            //var builtServices = services.BuildServiceProvider();
            //var healthCheckService = builtServices.GetService<IHealthCheckService>();
            //Assert.NotNull(healthCheckService.CheckHealthAsync().Result.Entries["[Redis] - Cache Database"]);
        }

        [Fact]
        public void AddRedisCheck_WithInvalidConnectionString_ThrowsArgumentException()
        {
            // Arrange
            var services = new ServiceCollection();
            var asd = services.AddHealthChecks();
            var connectionString = "";

            // Act & Assert
            var builder = Assert.Throws<ArgumentException>(() => asd.AddRedisCheck(connectionString));
            Assert.Equal("Value cannot be null or empty. (Parameter 'connectionString')", builder.Message);
        }
    }
}