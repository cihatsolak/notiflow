namespace Puzzle.Lib.MessageBroker.Tests
{
    public class ServiceCollectionContainerBuilderExtensionsTests
    {
        [Fact]
        public void AddRabbitMqSetting_ShouldConfigureRabbitMqSetting()
        {
            // Arrange
            var services = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                {"RabbitMqSetting:HostName", "localhost"},
                {"RabbitMqSetting:Username", "guest"},
                {"RabbitMqSetting:Password", "guest"}
                })
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Act
            services.AddRabbitMqSetting();
            var serviceProvider = services.BuildServiceProvider();
            var rabbitMqSetting = serviceProvider.GetService<IOptions<RabbitMqSetting>>().Value;

            // Assert
            Assert.Equal("localhost", rabbitMqSetting.HostName);
            Assert.Equal("guest", rabbitMqSetting.Username);
            Assert.Equal("guest", rabbitMqSetting.Password);
        }

        [Fact]
        public void AddRabbitMqSetting_ShouldThrowArgumentNullException_WhenServiceProviderIsNull()
        {
            // Arrange
            IServiceCollection services = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => services.AddRabbitMqSetting());
        }
    }
}