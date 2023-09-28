namespace Puzzle.Lib.Performance.Tests
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseResponseCompress_AddsResponseCompressionMiddlewareToPipeline()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder.UseTestServer();

                    webHostBuilder.ConfigureServices(services =>
                    {
                        services.AddGzipResponseFastestCompress();
                    });

                    webHostBuilder.Configure(app =>
                    {
                        app.UseResponseCompress();
                    });
                });

            var host = hostBuilder.Start();
            var server = host.GetTestServer();

            // Act
            var response = server.CreateClient().GetAsync("/");

            // Assert
            Assert.NotNull(response);
        }
    }

}