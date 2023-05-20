namespace Puzzle.Lib.Auth.Tests.Extensions
{
    public class ClaimsPrincipalExtensionsTests
    {
        [Fact]
        public void Claims_WhenClaimsPrincipalHasMatchingClaim_ReturnsClaimValues()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cihat Solak"),
                new Claim(ClaimTypes.Email, "cihatsolak@hotmail.com")
            };

            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Act
            var emailClaim = claimsPrincipal.Claims(ClaimTypes.Email);

            // Assert
            Assert.Equal(new[] { "cihatsolak@hotmail.com" }, emailClaim);
        }

        [Fact]
        public void Claims_WhenClaimsPrincipalHasNoMatchingClaim_ReturnsEmpty()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cihat Solak"),
                new Claim(ClaimTypes.Country, "TR")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Act
            var emailClaims = claimsPrincipal.Claims(ClaimTypes.Email);

            // Assert
            Assert.Empty(emailClaims);
        }

        [Fact]
        public void ClaimRoles_WhenClaimsPrincipalHasRoleClaims_ReturnsRoleClaims()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cihat Solak"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Manager")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Act
            var roleClaims = claimsPrincipal.ClaimRoles();

            // Assert
            Assert.Equal(new[] { "Admin", "Manager" }, roleClaims);
        }

        [Fact]
        public void ClaimRoles_WhenClaimsPrincipalHasNoMatchingClaim_ReturnsEmpty()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cihat Solak"),
                new Claim(ClaimTypes.Country, "TR")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Act
            var roleClaims = claimsPrincipal.ClaimRoles();

            // Assert
            Assert.Empty(roleClaims);
        }

        [Fact]
        public void ClaimAudiences_WhenClaimsPrincipalHasAudienceClaims_ReturnsAudienceClaims()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cihat Solak"),
                new Claim(JwtRegisteredClaimNames.Aud, "api1"),
                new Claim(JwtRegisteredClaimNames.Aud, "api2")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Act
            var audienceClaims = claimsPrincipal.ClaimAudiences();

            // Assert
            Assert.Equal(new[] { "api1", "api2" }, audienceClaims);
        }

        [Fact]
        public void ClaimAudiences_WhenClaimsPrincipalHasNoMatchingClaim_ReturnsEmpty()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cihat Solak"),
                new Claim(ClaimTypes.Country, "TR")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Act
            var audienceClaims = claimsPrincipal.ClaimAudiences();

            // Assert
            Assert.Empty(audienceClaims);
        }
    }
}