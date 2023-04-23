using Notiflow.IdentityServer.Infrastructure.Data;
using Puzzle.Lib.Database.Interfaces;

namespace Notiflow.IdentityServer.Service.Auth
{
    public interface IAuthService
    {
        Task<ResponseModel<TokenResponse>> CreateTokenAsync(); 
    }

    internal class AuthManager : IAuthService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly ITenantService _tenantService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(ApplicationDbContext appDbContext, ITenantService tenantService, ITokenService tokenService, ILogger<AuthManager> logger)
        {
            _appDbContext = appDbContext;
            _tenantService = tenantService;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<ResponseModel<TokenResponse>> CreateTokenAsync()
        {
            var user = await _appDbContext.Users.FindAsync(4);
            if (user is null)
            {

            }

            var tokenResponse = _tokenService.CreateTokenByUser(user);
            if (!tokenResponse.Succeeded)
            {

            }

            return tokenResponse;
        }
    }
}
