namespace Puzzle.Lib.Auth.Tests.Extensions
{
    public class JwtTokenExtensionsTests
    {
        [Fact]
        public void CreateRefreshToken_ReturnsNewToken()
        {
            // Arrange + Act
            var token1 = JwtTokenExtensions.CreateRefreshToken();
            var token2 = JwtTokenExtensions.CreateRefreshToken();

            // Assert
            Assert.NotEqual(token1, token2);
        }
    }
}