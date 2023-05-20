namespace Puzzle.Lib.Auth.Tests.Middlewares
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseAuth_ShouldCallUseAuthenticationAndUseAuthorization()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAuthentication();
            services.AddAuthorization();
            var serviceProvider = services.BuildServiceProvider();

            var app = new ApplicationBuilder(serviceProvider);

            // Act
            app.UseAuth();

            // Assert
            Assert.True(app.Properties.ContainsKey("__AuthenticationMiddlewareSet"));
            Assert.True(app.Properties.ContainsKey("__AuthorizationMiddlewareSet"));
        }

        [Fact]
        public void UseAuth_WhenAppIsNull_ThrowArgumentNullException()
        {
            // Arrange
            IApplicationBuilder app = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => app.UseAuth());
            Assert.Equal("Value cannot be null. (Parameter 'app')", exception.Message);
        }
    }
}
