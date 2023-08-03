namespace Notiflow.Backoffice.Persistence.Seeds;

internal static class SeedData
{
    private static readonly Guid[] ExistingTenantTokens = new Guid[] 
    {
        new Guid("C7804183-349A-46B4-98F8-F0BF64D900B2"),
        new Guid("2E9DE9EC-18AF-4B62-BC94-65AD326C0769"),
        new Guid("F8964EF2-0AF0-43CE-BF76-3B86158A4D2B"),
        new Guid("DCA5789C-1555-4AAE-AD2B-CB90791D22C2"),
        new Guid("B03A8674-C64F-4BA8-89BC-A71C18E93865"),
        new Guid("B1CF9735-9A02-43BD-A81A-D195F51EBC77"),
        new Guid("64D5E2A4-361F-4139-AE93-E9B225B02131")
    };

    internal static List<Customer> GenerateCustomers()
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
            .RuleFor(customer => customer.TenantToken, f => f.Random.ArrayElement(ExistingTenantTokens))
            .Generate(120);
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
            .RuleFor(notificationHistory => notificationHistory.SenderIdentity, faker => Guid.NewGuid())
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
           .Generate(250);

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
           .Generate(50);
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
           .Generate(380);
    }
}