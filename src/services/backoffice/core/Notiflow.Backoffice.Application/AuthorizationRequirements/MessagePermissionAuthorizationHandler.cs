namespace Notiflow.Backoffice.Application.AuthorizationRequirements;

public sealed record MessagePermissionRequirement : IAuthorizationRequirement;

public sealed class MessagePermissionAuthorizationHandler : AuthorizationHandler<MessagePermissionRequirement>
{
    private readonly IRedisService _redisService;

    public MessagePermissionAuthorizationHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MessagePermissionRequirement requirement)
    {
        if (context?.User?.Identity == null)
        {
            context.Fail(new AuthorizationFailureReason(this, "selam"));
            return;
        }

        bool isMessagePermission = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_MESSAGE_PERMISSION);
        if (!isMessagePermission)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
