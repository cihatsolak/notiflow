using System.Xml.Linq;

namespace Puzzle.Lib.Auth.Tests.Services
{
    public class ClaimManagerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public ClaimManagerTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        [Theory]
        [InlineData("example@example.com")]
        [InlineData("cihatsolak@hotmail.com")]
        [Category("ClaimManager")]
        public void GetEmail_WhenUserHasEmailClaim_ReturnsEmail(string email)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(email, claimManager.Email);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetEmail_WhenEmailClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Email);
        }

        [Theory]
        [InlineData("Cihat")]
        [InlineData("Ali")]
        [Category("ClaimManager")]
        public void GetName_WhenUserHasNameClaim_ReturnsName(string name)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(name, claimManager.Name);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetName_WhenNameClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Name);
        }

        [Theory]
        [InlineData("Solak")]
        [Category("ClaimManager")]
        public void GetFamilyName_WhenUserHasFamilyNameClaim_ReturnsFamilyName(string familyName)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.FamilyName, familyName) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(familyName, claimManager.FamilyName);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetFamilyName_WhenFamilyNameClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.FamilyName);
        }

        [Theory]
        [InlineData("12")]
        [Category("ClaimManager")]
        public void GetNameIdentifier_WhenUserHasNameIdentifierClaim_ReturnsNameIdentifier(string nameIdentifier)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, nameIdentifier) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(int.Parse(nameIdentifier), claimManager.NameIdentifier);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetNameIdentifier_WhenUserHasInvalidNameIdentifierClaim_ReturnsNameIdentifier()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "-5") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.NameIdentifier);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetNameIdentifier_WhenNameIdentifierClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.NameIdentifier);
        }

        [Theory]
        [InlineData("administrator")]
        [Category("ClaimManager")]
        public void GetRole_WhenUserHasRoleClaim_ReturnsRole(string role)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(role, claimManager.Role);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetRole_WhenRoleClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Role);
        }
    }
}
