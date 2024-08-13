namespace Notiflow.Backoffice.Application.AuthorizationRequirements;

public sealed record MessagePermissionRequirement : IAuthorizationRequirement;

public sealed class MessagePermissionAuthorizationHandler(
    IRedisService redisService) : AuthorizationHandler<MessagePermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MessagePermissionRequirement requirement)
    {
        if (context?.User?.Identity is null)
        {
            context?.Fail();
            return;
        }

        bool isMessagePermission = await redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_MESSAGE_PERMISSION);
        if (!isMessagePermission)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
