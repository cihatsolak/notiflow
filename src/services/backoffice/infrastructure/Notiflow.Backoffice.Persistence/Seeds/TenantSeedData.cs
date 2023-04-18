namespace Notiflow.Backoffice.Persistence.Seeds;

internal static class TenantSeedData
{
    internal static List<Tenant> GenerateFakeTenants()
    {
        return new Faker<Tenant>("tr")
             .RuleFor(tenant => tenant.Name, faker => faker.Company.CompanyName())
             .RuleFor(tenant => tenant.AppId, faker => Guid.NewGuid())
             .RuleFor(tenant => tenant.Definition, faker => faker.Company.CatchPhrase())
             .RuleFor(tenant => tenant.TenantApplication, faker => GenerateTenantApplication())
             .RuleFor(tenant => tenant.TenantPermission, faker => GenerateTenantPermission())
             .RuleFor(tenant => tenant.Users, faker => GenerateUsers())
             .RuleFor(tenant => tenant.Customers, faker => GenerateCustomers())
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
                  .RuleFor(tenantApplication => tenantApplication.MailReplyAddress, faker => faker.Internet.Email());
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
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password, faker => faker.Internet.Password())
            .Generate(5);
    }

    private static List<Customer> GenerateCustomers()
    {
        return new Faker<Customer>("tr")
            .RuleFor(customer => customer.Name, faker => faker.Person.FirstName)
            .RuleFor(customer => customer.Surname, faker => faker.Person.LastName)
            .RuleFor(customer => customer.PhoneNumber, faker => faker.Phone.PhoneNumber("5#########"))
            .RuleFor(customer => customer.Email, faker => faker.Person.Email)
            .RuleFor(customer => customer.BirthDate, faker => faker.Person.DateOfBirth)
            .RuleFor(customer => customer.Gender, faker => faker.PickRandom<Gender>())
            .RuleFor(customer => customer.MarriageStatus, faker => faker.PickRandom<MarriageStatus>())
            .RuleFor(customer => customer.IsBlocked, faker => faker.Random.Bool())
            .RuleFor(customer => customer.IsDeleted, faker => faker.Random.Bool())
            .RuleFor(customer => customer.Device, faker => GenerateDevice())
            .RuleFor(customer => customer.TextMessageHistories, faker => GenerarateTextMessageHistories())
            .RuleFor(customer => customer.NotificationHistories, faker => GetNotificationHistories())
            .RuleFor(customer => customer.EmailHistories, faker => GenerateEmailHistories())
            .Generate(35);
    }

    private static Device GenerateDevice()
    {
        return new Faker<Device>("tr")
            .RuleFor(device => device.OSVersion, faker => faker.PickRandom<OSVersion>())
            .RuleFor(device => device.Code, faker => faker.Random.AlphaNumeric(54))
            .RuleFor(device => device.Token, faker => faker.Random.AlphaNumeric(145))
            .RuleFor(device => device.CloudMessagePlatform, faker => faker.PickRandom<CloudMessagePlatform>());
    }

    private static List<NotificationHistory> GetNotificationHistories()
    {
        return new Faker<NotificationHistory>("tr")
            .RuleFor(notificationHistory => notificationHistory.Title, faker => faker.Lorem.Sentence(3))
            .RuleFor(notificationHistory => notificationHistory.Message, faker => faker.Lorem.Sentence(10))
            .RuleFor(notificationHistory => notificationHistory.IsSent, faker => faker.Random.Bool())
            .RuleFor(notificationHistory => notificationHistory.ErrorMessage, (faker, notificationHistory) =>
            {
                if (notificationHistory.IsSent)
                {
                    return null;
                }
                else
                {
                    return faker.Lorem.Sentence();
                }
            })
           .RuleFor(notificationHistory => notificationHistory.SentDate, faker => faker.Date.Between(DateTime.Now.AddDays(-90), DateTime.Now.AddMinutes(-15)))
           .Generate(60);

    }

    private static List<EmailHistory> GenerateEmailHistories()
    {
        return new Faker<EmailHistory>("tr")
            .RuleFor(emailHistory => emailHistory.Recipients, faker => faker.Internet.Email())
            .RuleFor(emailHistory => emailHistory.Cc, faker => string.Join(";", Enumerable.Range(1, 5).Select(index => faker.Internet.Email())))
            .RuleFor(emailHistory => emailHistory.Bcc, faker => string.Join(";", Enumerable.Range(1, 2).Select(index => faker.Internet.Email())))
            .RuleFor(emailHistory => emailHistory.Subject, faker => faker.Lorem.Sentence(4))
            .RuleFor(emailHistory => emailHistory.Body, faker => faker.Lorem.Sentence(50))
            .RuleFor(emailHistory => emailHistory.IsSent, faker => faker.Random.Bool())
            .RuleFor(emailHistory => emailHistory.ErrorMessage, (faker, emailHistory) =>
            {
                if (emailHistory.IsSent)
                {
                    return null;
                }
                else
                {
                    return faker.Lorem.Sentence();
                }
            })
           .RuleFor(emailHistory => emailHistory.SentDate, faker => faker.Date.Between(DateTime.Now.AddDays(-90), DateTime.Now.AddMinutes(-15)))
           .Generate(20);
    }

    private static List<TextMessageHistory> GenerarateTextMessageHistories()
    {
        return new Faker<TextMessageHistory>("tr")
            .RuleFor(textMessageHistory => textMessageHistory.Message, faker => faker.Lorem.Sentence(10))
            .RuleFor(textMessageHistory => textMessageHistory.IsSent, faker => faker.Random.Bool())
            .RuleFor(textMessageHistory => textMessageHistory.ErrorMessage, (faker, textMessageHistory) =>
            {
                if (textMessageHistory.IsSent)
                {
                    return null;
                }
                else
                {
                    return faker.Lorem.Sentence();
                }
            })
           .RuleFor(textMessageHistory => textMessageHistory.SentDate, faker => faker.Date.Between(DateTime.Now.AddDays(-90), DateTime.Now.AddMinutes(-15)))
           .Generate(50);
    }
}