using Puzzle.Lib.Host.Infrastructure;

namespace Puzzle.Lib.Host.Tests
{
    public class HostBuilderExtensionsTests
    {
        [Fact]
        public void AddServiceValidateScope_ValidateScopes_Set_To_True_In_Development_Environment()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureServices(services =>
                {
                    // Add a dummy service
                    services.AddSingleton<IDummyService, DummyService>();
                })
                .AddServiceValidateScope()
                .UseEnvironment("Development");

            // Act
            var host = hostBuilder.Build();
            var serviceProvider = host.Services.CreateScope().ServiceProvider;

            // Assert
            Assert.NotNull(serviceProvider.GetService<IDummyService>());
        }

        [Fact]
        public void AddServiceValidateScope_ValidateScopes_Set_To_False_In_Production_Environment()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureServices(services =>
                {
                    // Add a dummy service
                    services.AddSingleton<IDummyService, DummyService>();
                })
                .AddServiceValidateScope()
                .UseEnvironment("Production");

            // Act
            var host = hostBuilder.Build();
            var serviceProvider = host.Services.CreateScope().ServiceProvider;

            // Assert
            Assert.NotNull(serviceProvider.GetService<IDummyService>());
        }
    }

    public interface IDummyService { }

    public class DummyService : IDummyService { }
}