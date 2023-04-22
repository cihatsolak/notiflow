namespace Notiflow.IdentityServer.Customization;

public sealed class CustomUserValidator : IUserValidator<AppUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
    {
        List<IdentityError> identityErrors = new();

        bool isDigit = int.TryParse(user.UserName[0].ToString(), out _);
        if (isDigit)
        {
            identityErrors.Add(new IdentityError()
            {
                Code = "UserNameContainFirstLetterDigit",
                Description = "Kullanıcı adının ilk karekteri sayısal bir karakter içeremez"
            });
        }

        if (identityErrors.Any())
        {
            return Task.FromResult(IdentityResult.Failed(identityErrors.ToArray()));
        }

        return Task.FromResult(IdentityResult.Success);
    }
}