﻿namespace Notiflow.Backoffice.Persistence.Seeds;

internal static class SeedData
{
    private static readonly int[] ExistingTenantIds = new int[]
    {
        1,2,3,4,5,6,7
    };

    internal static List<Customer> GenerateCustomers()
    {
        return new Faker<Customer>("tr")
            .RuleFor(customer => customer.Name, faker => faker.Person.FirstName)
            .RuleFor(customer => customer.Surname, faker => faker.Person.LastName)
            .RuleFor(customer => customer.PhoneNumber, faker => faker.Phone.PhoneNumber("53########"))
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
            .RuleFor(customer => customer.TenantId, f => f.Random.ArrayElement(ExistingTenantIds))
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
            .RuleFor(notificationHistory => notificationHistory.ImageUrl, faker => faker.Internet.Avatar())
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
            .RuleFor(emailHistory => emailHistory.Recipients, faker => string.Join(";", Enumerable.Range(1, 3).Select(index => faker.Internet.Email())))
            .RuleFor(emailHistory => emailHistory.Cc, faker => string.Join(";", Enumerable.Range(1, 3).Select(index => faker.Internet.Email())))
            .RuleFor(emailHistory => emailHistory.Bcc, faker => string.Join(";", Enumerable.Range(1, 2).Select(index => faker.Internet.Email())))
            .RuleFor(emailHistory => emailHistory.Subject, faker => faker.Lorem.Sentence(4))
            .RuleFor(emailHistory => emailHistory.IsBodyHtml, faker => faker.Random.Bool())
            .RuleFor(emailHistory => emailHistory.Body, (faker, emailHistory) =>
            {
                if (emailHistory.IsBodyHtml)
                {
                    return RandomHtml(faker);
                }
                else
                {
                    return faker.Lorem.Sentence(250);
                }
            })
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
           .RuleFor(emailHistory => emailHistory.SentDate, faker => faker.Date.Between(DateTime.Now.AddDays(-90), DateTime.Now.AddDays(-5)))
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

    private static string RandomHtml(Faker faker)
    {
        string title = faker.Lorem.Sentence();
        string paragraph1 = faker.Lorem.Paragraph();
        string paragraph2 = faker.Lorem.Paragraph();
        string listItem1 = faker.Lorem.Word();
        string listItem2 = faker.Lorem.Word();

        StringBuilder htmlBuilder = new();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("<title>" + title + "</title>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h1>" + title + "</h1>");
        htmlBuilder.AppendLine("<p>" + paragraph1 + "</p>");
        htmlBuilder.AppendLine("<p>" + paragraph2 + "</p>");
        htmlBuilder.AppendLine("<ul>");
        htmlBuilder.AppendLine("<li>" + listItem1 + "</li>");
        htmlBuilder.AppendLine("<li>" + listItem2 + "</li>");
        htmlBuilder.AppendLine("</ul>");
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        return htmlBuilder.ToString();
    }
}