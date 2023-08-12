namespace Notiflow.Backoffice.Application.AuthorizationRequirements;

public sealed record UserNameIdentifierRequirement : IAuthorizationRequirement
{
}

public sealed class UserNameIdentifierAuthorizationHandler : AuthorizationHandler<UserNameIdentifierRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserNameIdentifierRequirement requirement)
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
