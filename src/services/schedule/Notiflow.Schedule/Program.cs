var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

SeriLogElasticSetting seriLogElasticSetting = builder.Configuration.GetRequiredSection(nameof(SeriLogElasticSetting)).Get<SeriLogElasticSetting>();

builder.Host.AddSeriLogWithElasticSearch(options =>
{
    options.Address = seriLogElasticSetting.Address;
    options.Username = seriLogElasticSetting.Username;
    options.Password = seriLogElasticSetting.Password;
    options.IsRequiredAuthentication = seriLogElasticSetting.IsRequiredAuthentication;
});

builder.AddDependencies();
builder.AddConfigureHealthChecks();

builder.Services
    .AddMassTransit()
    .AddLowercaseRouting()
    .AddLocalize()
    .AddGzipResponseFastestCompress()
    .AddFluentDesignValidation()
    .AddHttpSecurityPrecautions(builder.Environment)
    .AddCustomHttpLogging();

var app = builder.Build();

app
   .UseHttpSecurityPrecautions(builder.Environment)
   .UseAuth()
   .UseSwaggerWithRedoclyDoc(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseApiExceptionHandler()
   .UseResponseCompress()
   .UseSerilogLogging()
   .UseCustomHttpLogging()
   .UseHealthChecksConfiguration()
   .UseHangfire();

app.UseLocalizationWithEndpoint();
app.MapControllers();

RecurringJob.AddOrUpdate<ScheduledTextMessageSendingRecurringJob>("475fe763-e667-467f-b373-bcdfc2e1deab", recurring => recurring.ExecuteAsync(), "*/5 * * * *");
RecurringJob.AddOrUpdate<ScheduledNotificationSendingRecurringJob>("5a27bb09-62d6-4928-bff8-58bac25e69f6", recurring => recurring.ExecuteAsync(), "*/5 * * * *");
RecurringJob.AddOrUpdate<ScheduledEmailSendingRecurringJob>("4eaf3580-abbe-4728-8b3a-7606332d959b", recurring => recurring.ExecuteAsync(), "*/5 * * * *");

await app.StartProjectAsync();