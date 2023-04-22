namespace Notiflow.IdentityServer.Infrastructure;

public sealed class CustomPasswordValidator : IPasswordValidator<AppUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
    {
        List<IdentityError> identityErrors = new();

        if (password.Equals(user.UserName, StringComparison.OrdinalIgnoreCase))
        {
            identityErrors.Add(new IdentityError
            {
                Code = "PasswordContainUserName",
                Description = "Şifre alanı kullanıcı adı içeremez."
            });
        }

        if (password.StartsWith("1234"))
        {
            identityErrors.Add(new IdentityError
            {
                Code = "PasswordContain1234",
                Description = "Şifre alanı ardışık sayı içeremez."
            });
        }

        if (identityErrors.Any())
        {
            return Task.FromResult(IdentityResult.Failed(identityErrors.ToArray()));
        }

        return Task.FromResult(IdentityResult.Success);
    }
}
