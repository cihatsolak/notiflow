var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.AddDependencies();

builder.Services
    .AddMassTransit()
    .AddLowercaseRouting()
    .AddLocalize()
    .AddGzipResponseFastestCompress()
    .AddHttpSecurityPrecautions(builder.Environment);

var app = builder.Build();

app
   .UseHttpSecurityPrecautions(builder.Environment)
   .UseAuth()
   .UseSwaggerWithRedoclyDoc(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseApiExceptionHandler()
   .UseResponseCompress()
   .UseHealthChecksConfiguration()
   .UseHangfire();

app.UseLocalizationWithEndpoint();
app.MapControllers();

RecurringJob.AddOrUpdate<ScheduledTextMessageSendingRecurringJob>("475fe763-e667-467f-b373-bcdfc2e1deab", recurring => recurring.ExecuteAsync(), "*/5 * * * *");
RecurringJob.AddOrUpdate<ScheduledNotificationSendingRecurringJob>("5a27bb09-62d6-4928-bff8-58bac25e69f6", recurring => recurring.ExecuteAsync(), "*/5 * * * *");
RecurringJob.AddOrUpdate<ScheduledEmailSendingRecurringJob>("4eaf3580-abbe-4728-8b3a-7606332d959b", recurring => recurring.ExecuteAsync(), "*/5 * * * *");

await app.StartProjectAsync();