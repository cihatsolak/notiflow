using Microsoft.Extensions.DependencyInjection;

namespace Puzzle.Lib.Auth.Tests.Middlewares
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseAuth_AddsAuthenticationAndAuthorizationMiddleware()
        {
            // Arrange
            var mockApp = new Mock<IApplicationBuilder>();
            var mockServices = new Mock<IServiceCollection>();
            var mockServicess = new Mock<IServiceProvider>();
            mockApp.Setup(m => m.ApplicationServices).Returns(mockServicess.Object);

            // Act
            var result = ApplicationBuilderExtensions.UseAuth(mockApp.Object);

            // Assert
            Assert.Equal(mockApp.Object, result);
            mockServices.Verify(m => m.AddAuthentication(), Times.Once);
            mockServices.Verify(m => m.AddAuthorization(), Times.Once);
            mockApp.Verify(m => m.UseAuthentication(), Times.Once);
            mockApp.Verify(m => m.UseAuthorization(), Times.Once);
        }
    }
}
