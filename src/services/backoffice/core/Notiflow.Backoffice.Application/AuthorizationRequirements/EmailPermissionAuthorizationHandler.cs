namespace Notiflow.Backoffice.Application.AuthorizationRequirements;

public sealed record EmailPermissionRequirement : IAuthorizationRequirement;

public sealed class EmailPermissionAuthorizationHandler : AuthorizationHandler<EmailPermissionRequirement>
{
    private readonly IRedisService _redisService;

    public EmailPermissionAuthorizationHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailPermissionRequirement requirement)
    {
        if (context?.User?.Identity == null)
        {
            context.Fail();
            return;
        }

        bool isEmailPermission = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_EMAIL_PERMISSION);
        if (!isEmailPermission)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
