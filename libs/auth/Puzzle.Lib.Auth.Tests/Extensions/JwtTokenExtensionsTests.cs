namespace Puzzle.Lib.Auth.Tests.Extensions
{
    public class JwtTokenExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        public void CreateSecurityKey_WhenKeyIsNull_ThrowsArgumentNullException(string key)
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => JwtTokenExtensions.CreateSecurityKey(key));
        }

        [Theory]
        [InlineData("")]
        public void CreateSecurityKey_WhenKeyIsEmpty_ThrowsArgumentException(string key)
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentException>(() => JwtTokenExtensions.CreateSecurityKey(key));
        }

        [Fact]
        public void CreateSigningCredentials_WhenSecurityKeyIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            SecurityKey securityKey = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => JwtTokenExtensions.CreateSigningCredentials(securityKey));
        }

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