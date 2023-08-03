namespace Notiflow.IdentityServer.Data.Seeds;

internal static class SeedData
{
    internal static List<Tenant> GenerateFakeTenants()
    {
        return new Faker<Tenant>("tr")
             .RuleFor(tenant => tenant.Name, faker => faker.Company.CompanyName())
             .RuleFor(tenant => tenant.Token, faker => Guid.NewGuid())
             .RuleFor(tenant => tenant.Definition, faker => faker.Company.CatchPhrase())
             .RuleFor(tenant => tenant.TenantApplication, faker => GenerateTenantApplication())
             .RuleFor(tenant => tenant.TenantPermission, faker => GenerateTenantPermission())
             .RuleFor(tenant => tenant.Users, faker => GenerateUsers())
             .Generate(7);
    }

    private static TenantApplication GenerateTenantApplication()
    {
        return new Faker<TenantApplication>("tr")
                  .RuleFor(tenantApplication => tenantApplication.FirebaseServerKey, faker => $"AAA{faker.Random.AlphaNumeric(149)}")
                  .RuleFor(tenantApplication => tenantApplication.FirebaseSenderId, faker => faker.Random.AlphaNumeric(11))
                  .RuleFor(tenantApplication => tenantApplication.HuaweiServerKey, faker => $"AAA{faker.Random.AlphaNumeric(41)}")
                  .RuleFor(tenantApplication => tenantApplication.HuaweiSenderId, faker => faker.Random.AlphaNumeric(12))
                  .RuleFor(tenantApplication => tenantApplication.MailFromAddress, faker => faker.Internet.Email())
                  .RuleFor(tenantApplication => tenantApplication.MailFromName, faker => faker.Internet.UserName())
                  .RuleFor(tenantApplication => tenantApplication.MailReplyAddress, faker => faker.Internet.Email())
                  .RuleFor(tenantApplication => tenantApplication.MailSmtpHost, faker => faker.Internet.DomainName())
                  .RuleFor(tenantApplication => tenantApplication.MailSmtpPort, faker => faker.Random.Int(1024, 65535));
    }

    private static TenantPermission GenerateTenantPermission()
    {
        return new Faker<TenantPermission>("tr")
                  .RuleFor(tenantPermission => tenantPermission.IsSendMessagePermission, faker => faker.Random.Bool())
                  .RuleFor(tenantPermission => tenantPermission.IsSendNotificationPermission, faker => faker.Random.Bool())
                  .RuleFor(tenantPermission => tenantPermission.IsSendEmailPermission, faker => faker.Random.Bool());
    }

    private static List<User> GenerateUsers()
    {
        return new Faker<User>("tr")
            .RuleFor(user => user.Name, faker => faker.Person.FirstName)
            .RuleFor(user => user.Surname, faker => faker.Person.LastName)
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password, faker => faker.Internet.Password())
            .RuleFor(user => user.UserRefreshToken, faker => GenerateUserRefreshToken())
            .Generate(5);
    }

    private static UserRefreshToken GenerateUserRefreshToken()
    {
        return new Faker<UserRefreshToken>("tr")
            .RuleFor(userRefreshToken => userRefreshToken.Token, faker => faker.Random.AlphaNumeric(50))
            .RuleFor(userRefreshToken => userRefreshToken.ExpirationDate, faker => faker.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100)));
    }
}
