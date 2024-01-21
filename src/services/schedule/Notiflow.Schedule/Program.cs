var builder = WebApplication.CreateBuilder(args);

builder.HostConfigured();

builder.AddWebDependencies();
builder.AddConfigureHealthChecks();

builder.Services
    .AddMassTransit()
    .AddLowercaseRoute()
    .AddWebApiLocalize()
    .AddResponseCompression()
    .AddServerSideValidation()
    .AddCustomHttpLogging();

if (!builder.Environment.IsProduction())
{
    builder.Services.AddHttpSecurityPrecautions();
}

var app = builder.Build();

bool isProduction = app.Environment.IsProduction();
if (isProduction)
{
    app.UseHttpSecurityPrecautions();
}
else
{
    app.UseSwaggerRedocly()
       .UseMigrations();
}

app.UseApiExceptionHandler()
   .UseAuth()
   .UseResponseCompression()
   .UseSerilogLogging()
   .UseCustomHttpLogging()
   .UseHealth()
   .UseHangfire();

app.UseLocalizationWithEndpoint();
app.MapControllers();

RecurringJob.AddOrUpdate<ScheduledTextMessageSendingRecurringJob>("475fe763-e667-467f-b373-bcdfc2e1deab", recurring => recurring.ExecuteAsync(), "*/5 * * * *");
RecurringJob.AddOrUpdate<ScheduledNotificationSendingRecurringJob>("5a27bb09-62d6-4928-bff8-58bac25e69f6", recurring => recurring.ExecuteAsync(), "*/5 * * * *");
RecurringJob.AddOrUpdate<ScheduledEmailSendingRecurringJob>("4eaf3580-abbe-4728-8b3a-7606332d959b", recurring => recurring.ExecuteAsync(), "*/5 * * * *");

await app.StartProjectAsync();