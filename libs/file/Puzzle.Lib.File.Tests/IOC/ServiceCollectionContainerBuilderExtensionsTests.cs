using Puzzle.Lib.File.Settings;

namespace Puzzle.Lib.File.Tests.IOC
{
    public class ServiceCollectionContainerBuilderExtensionsTests
    {
        [Fact]
        public void AddFtpSetting_Should_Configure_FtpSetting_Options()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"FtpSetting:Ip", "127.0.0.1"},
                    {"FtpSetting:Username", "user@example.com"},
                    {"FtpSetting:Password", "password123"},
                })
                .Build();

            var services = new ServiceCollection()
                .AddOptions()
                .AddSingleton<IConfiguration>(configuration);

            // Act
            //services.AddFtpSetting();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var ftpSetting = serviceProvider.GetService<IOptions<FtpSetting>>().Value;

            Assert.Equal("127.0.0.1", ftpSetting.Ip);
            Assert.Equal("user@example.com", ftpSetting.Username);
            Assert.Equal("password123", ftpSetting.Password);
        }
    }
}
