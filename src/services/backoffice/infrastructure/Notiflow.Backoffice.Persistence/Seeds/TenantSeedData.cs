namespace Notiflow.Backoffice.Persistence.Seeds;

internal static class TenantSeedData
{
    internal static List<Tenant> GenerateFakeTenants()
    {
        return new Faker<Tenant>("tr")
             .RuleFor(p => p.Name, p => p.Company.CompanyName())
             .RuleFor(p => p.AppId, p => Guid.NewGuid())
             .RuleFor(p => p.Definition, p => p.Company.CatchPhrase())
             .RuleFor(p => p.TenantApplication, p => GenerateTenantApplication())
             .RuleFor(p => p.TenantPermission, p => GenerateTenantPermission())
             .RuleFor(p => p.Users, p => GenerateUsers())
             .RuleFor(p => p.Customers, p => GenerateCustomers())
             .Generate(7);
    }

    private static TenantApplication GenerateTenantApplication()
    {
        return new Faker<TenantApplication>("tr")
                  .RuleFor(p => p.FirebaseServerKey, p => $"AAA{p.Random.AlphaNumeric(149)}")
                  .RuleFor(p => p.FirebaseSenderId, p => p.Random.AlphaNumeric(11))
                  .RuleFor(p => p.HuaweiServerKey, p => $"AAA{p.Random.AlphaNumeric(41)}")
                  .RuleFor(p => p.HuaweiSenderId, p => p.Random.AlphaNumeric(12))
                  .RuleFor(p => p.MailFromAddress, p => p.Internet.Email())
                  .RuleFor(p => p.MailFromName, p => p.Internet.UserName())
                  .RuleFor(p => p.MailReplyAddress, p => p.Internet.Email());
    }

    private static TenantPermission GenerateTenantPermission()
    {
        return new Faker<TenantPermission>("tr")
                  .RuleFor(p => p.IsSendMessagePermission, p => p.Random.Bool())
                  .RuleFor(p => p.IsSendNotificationPermission, p => p.Random.Bool())
                  .RuleFor(p => p.IsSendEmailPermission, p => p.Random.Bool());
    }

    private static List<User> GenerateUsers()
    {
        return new Faker<User>("tr")
            .RuleFor(p => p.Username, p => p.Internet.UserName())
            .RuleFor(p => p.Password, p => p.Internet.Password())
            .Generate(4);
    }

    private static List<Customer> GenerateCustomers()
    {
        return new Faker<Customer>("tr")
            .RuleFor(p => p.Name, p => p.Person.FirstName)
            .RuleFor(p => p.Surname, p => p.Person.LastName)
            .RuleFor(p => p.PhoneNumber, p => p.Person.Phone)
            .RuleFor(p => p.Email, p => p.Person.Email)
            .RuleFor(p => p.BirthDate, p => p.Person.DateOfBirth)
            .RuleFor(p => p.Gender, p => p.PickRandom<Gender>())
            .RuleFor(p => p.MarriageStatus, p => p.PickRandom<MarriageStatus>())
            .RuleFor(p => p.IsBlocked, p => p.Random.Bool())
            .RuleFor(p => p.IsDeleted, p => p.Random.Bool())
            .RuleFor(p => p.Device, p => GenerateDevice())
            .RuleFor(p => p.TextMessageHistories, p => GenerarateTextMessageHistories())
            .RuleFor(p => p.NotificationHistories, p => GetNotificationHistories())
            .RuleFor(p => p.EmailHistories, p => GenerateEmailHistories())
            .Generate(30);
    }

    private static Device GenerateDevice()
    {
        return new Faker<Device>("tr")
            .RuleFor(p => p.OSVersion, p => p.PickRandom<OSVersion>())
            .RuleFor(p => p.Code, p => p.Random.AlphaNumeric(54))
            .RuleFor(p => p.Token, p => p.Random.AlphaNumeric(145))
            .RuleFor(p => p.CloudMessagePlatform, p => p.PickRandom<CloudMessagePlatform>());
    }

    private static List<NotificationHistory> GetNotificationHistories()
    {
        return new Faker<NotificationHistory>("tr")
            .RuleFor(p => p.Title, p => p.Lorem.Sentence(3))
            .RuleFor(p => p.Message, p => p.Lorem.Sentence(10))
            .RuleFor(p => p.IsSent, p => p.Random.Bool())
            .RuleFor(p => p.ErrorMessage, (p, history) =>
            {
                if (history.IsSent)
                {
                    return p.Lorem.Sentence();
                }
                else
                {
                    return null;
                }
            })
           .RuleFor(p => p.SentDate, p => p.Date.Between(DateTime.Now.AddDays(-90), DateTime.Now.AddMinutes(-15)))
           .Generate(50);

    }

    private static List<EmailHistory> GenerateEmailHistories()
    {
        return new Faker<EmailHistory>("tr")
            .RuleFor(p => p.Recipients, p => p.Internet.Email())
            .RuleFor(p => p.Cc, p => string.Join(";", Enumerable.Range(1, 5).Select(index => p.Internet.Email())))
            .RuleFor(p => p.Bcc, p => string.Join(";", Enumerable.Range(1, 2).Select(index => p.Internet.Email())))
            .RuleFor(p => p.Subject, p => p.Lorem.Sentence(4))
            .RuleFor(p => p.Body, p => p.Lorem.Sentence(50))
            .RuleFor(p => p.IsSent, p => p.Random.Bool())
            .RuleFor(p => p.ErrorMessage, (p, history) =>
            {
                if (history.IsSent)
                {
                    return p.Lorem.Sentence();
                }
                else
                {
                    return null;
                }
            })
           .RuleFor(p => p.SentDate, p => p.Date.Between(DateTime.Now.AddDays(-90), DateTime.Now.AddMinutes(-15)))
           .Generate(10);
    }

    private static List<TextMessageHistory> GenerarateTextMessageHistories()
    {
        return new Faker<TextMessageHistory>("tr")
            .RuleFor(p => p.Message, p => p.Lorem.Sentence(10))
            .RuleFor(p => p.IsSent, p => p.Random.Bool())
            .RuleFor(p => p.ErrorMessage, (p, history) =>
            {
                if (history.IsSent)
                {
                    return p.Lorem.Sentence();
                }
                else
                {
                    return null;
                }
            })
           .RuleFor(p => p.SentDate, p => p.Date.Between(DateTime.Now.AddDays(-90), DateTime.Now.AddMinutes(-15)))
           .Generate(35);
    }
}
