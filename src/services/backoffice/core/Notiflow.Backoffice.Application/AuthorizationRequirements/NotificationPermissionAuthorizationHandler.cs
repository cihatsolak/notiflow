namespace Notiflow.Backoffice.Application.AuthorizationRequirements;

public sealed record NotificationPermissionRequirement : IAuthorizationRequirement;

public sealed class NotificationPermissionAuthorizationHandler : AuthorizationHandler<NotificationPermissionRequirement>
{
    private readonly IRedisService _redisService;

    public NotificationPermissionAuthorizationHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotificationPermissionRequirement requirement)
    {
        if (context?.User?.Identity is null)
        {
            context.Fail();
            return;
        }

        bool isNotificationPermission = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_NOTIFICATION_PERMISSION);
        if (!isNotificationPermission)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
