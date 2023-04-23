using Microsoft.EntityFrameworkCore;
using Notiflow.IdentityServer.Infrastructure.Data;

namespace Notiflow.IdentityServer.Service.Auth
{
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(p => p.Id == 4 && p.Tenant.Token == _tenantService.Token);
            if (user is null)
            {
                return ResponseModel<TokenResponse>.Fail(-1);
            }

            var tokenResponse = _tokenService.CreateTokenByUser(user);
            if (!tokenResponse.Succeeded)
            {
                return ResponseModel<TokenResponse>.Fail(-1);
            }

            return tokenResponse;
        }
    }
}
