namespace Puzzle.Lib.Auth.Tests.Services
{
    public class ClaimManagerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;


        public ClaimManagerTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        [Fact]
        public void GetEmail_ShouldReturnEmail_WhenClaimExists()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, "example@example.com") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act
            var email = claimManager.Email;

            // Assert
            Assert.Equal("example@example.com", email);
        }

        [Fact]
        public void GetEmail_ShouldThrowClaimException_WhenClaimDoesNotExist()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Email);
        }

        [Fact]
        public void GetName_ShouldReturnName_WhenClaimExists()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Cihat") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act
            var name = claimManager.Name;

            // Assert
            Assert.Equal("Cihat", name);
        }

        [Fact]
        public void GetName_ShouldThrowClaimException_WhenClaimDoesNotExist()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Name);
        }


    }

}
