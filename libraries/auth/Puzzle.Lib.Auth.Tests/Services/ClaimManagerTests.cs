using Puzzle.Lib.Auth.Tests.Data;
using System.Linq;
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

        [Theory]
        [ClassData(typeof(RolesTestDataGenerator))]
        [Category("ClaimManager")]
        public void GetRoles_WhenUserHasRolesClaim_ReturnsRoles(List<string> roles)
        {
            // Arrange
            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(roles, claimManager.Roles);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetRoles_WhenRolesClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Roles);
        }

        [Theory]
        [InlineData("c0dedadc-8b78-43a7-a823-ef4c4bedf104")]
        [Category("ClaimManager")]
        public void GetJti_WhenUserHasJtiClaim_ReturnsJti(string jti)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Jti, jti) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(jti, claimManager.Jti);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetJti_WhenJtiClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Jti);
        }

        [Theory]
        [InlineData("notiflow.com.tr")]
        [Category("ClaimManager")]
        public void GetAudience_WhenUserHasAudienceClaim_ReturnsAudience(string audience)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Aud, audience) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(audience, claimManager.Audience);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetAudience_WhenAudienceClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Audience);
        }

        [Theory]
        [ClassData(typeof(AudiencesTestDataGenerator))]
        [Category("ClaimManager")]
        public void GetAudiences_WhenUserHasAudiencesClaim_ReturnsAudiences(List<string> audiences)
        {
            // Arrange
            var claims = audiences.Select(role => new Claim(JwtRegisteredClaimNames.Aud, role));
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(audiences, claimManager.Audiences);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetAudiences_WhenAudiencesClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Audiences);
        }

        [Theory]
        [InlineData("cihatsolak")]
        [Category("ClaimManager")]
        public void GetGivenName_WhenUserHasGivenNameClaim_ReturnsAudience(string givenName)
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.GivenName, givenName) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(givenName, claimManager.GivenName);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetGivenName_WhenGivenNameClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.GivenName);
        }

        [Theory]
        [InlineData(2023, 5, 1)]
        [Category("ClaimManager")]
        public void GetIat_WhenUserHasIatClaim_ReturnsIat(int year, int month, int day)
        {
            // Arrange
            DateTime sampleIatDate = new(year, month, day);
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Iat, sampleIatDate.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(sampleIatDate, claimManager.Iat);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetIat_WhenUserHasInvalidIatClaim_ReturnsIat()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Iat, "8995/211/112") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Iat);
        }


        [Fact]
        [Category("ClaimManager")]
        public void GetIat_WhenIatClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.Iat);
        }

        //cihat

        [Theory]
        [InlineData(1996, 7, 16)]
        [Category("ClaimManager")]
        public void GetBirthDate_WhenUserHasBirthDateClaim_ReturnsBirthDate(int year, int month, int day)
        {
            // Arrange
            DateTime sampleBirthDate = new(year, month, day);
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Birthdate, sampleBirthDate.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Equal(sampleBirthDate, claimManager.BirthDate);
        }

        [Fact]
        [Category("ClaimManager")]
        public void GetBirthDate_WhenUserHasInvalidBirthDateClaim_ReturnsBirthDate()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Iat, "8995/211/112") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.BirthDate);
        }


        [Fact]
        [Category("ClaimManager")]
        public void GetBirthDate_WhenBirthDateClaimIsMissing_ThrowsClaimException()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var claimManager = new ClaimManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ClaimException>(() => claimManager.BirthDate);
        }
    }
}
