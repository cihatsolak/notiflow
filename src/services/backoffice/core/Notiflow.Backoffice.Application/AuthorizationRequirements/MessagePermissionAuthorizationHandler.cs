namespace Notiflow.Backoffice.Application.AuthorizationRequirements;

public sealed record MessagePermissionRequirement : IAuthorizationRequirement
{

}

public sealed class MessagePermissionAuthorizationHandler : AuthorizationHandler<MessagePermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MessagePermissionRequirement requirement)
    {
        if (context.User == null && context.User.Identity == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
