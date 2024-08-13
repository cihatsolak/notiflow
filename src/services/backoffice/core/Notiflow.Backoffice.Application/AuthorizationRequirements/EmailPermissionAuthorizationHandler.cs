namespace Notiflow.Backoffice.Application.AuthorizationRequirements;

public sealed record EmailPermissionRequirement : IAuthorizationRequirement;

public sealed class EmailPermissionAuthorizationHandler(
    IRedisService redisService) : AuthorizationHandler<EmailPermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailPermissionRequirement requirement)
    {
        if (context?.User?.Identity is null)
        {
            context?.Fail();
            return;
        }

        bool isEmailPermission = await redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_EMAIL_PERMISSION);
        if (!isEmailPermission)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
