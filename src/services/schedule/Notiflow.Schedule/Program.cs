var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services.AddControllers();

SqlSetting sqlSetting = builder.Configuration.GetRequiredSection(nameof(ScheduleDbContext)).Get<SqlSetting>();

builder.Services.AddMicrosoftSql<ScheduleDbContext>(options =>
{
    options.ConnectionString = sqlSetting.ConnectionString;
    options.CommandTimeoutSecond = sqlSetting.CommandTimeoutSecond;
    options.IsSplitQuery = sqlSetting.IsSplitQuery;
});

HangfireSetting hangfireSetting = builder.Configuration.GetRequiredSection(nameof(HangfireSetting)).Get<HangfireSetting>();

builder.Services.AddHangfireMsSql(options =>
{
    options.ConnectionString = hangfireSetting.ConnectionString;
    options.GlobalAutomaticRetryAttempts = hangfireSetting.GlobalAutomaticRetryAttempts;
    options.Username = hangfireSetting.Username;
    options.Password = hangfireSetting.Password;
});

SwaggerSetting swaggerSetting = builder.Configuration.GetRequiredSection(nameof(SwaggerSetting)).Get<SwaggerSetting>();

builder.Services.AddSwagger(options =>
{
    options.Title = swaggerSetting.Title;
    options.Description = swaggerSetting.Description;
    options.Version = swaggerSetting.Version;
    options.ContactName = swaggerSetting.ContactName;
    options.ContactEmail = swaggerSetting.ContactEmail;
});

builder.Services.AddMassTransit();

var app = builder.Build();

app.UseSwaggerWithRedoclyDoc(builder.Environment);
app.UseHangfire();
app.MapControllers();

RecurringJob.AddOrUpdate<ScheduledTextMessageSendingRecurringJob>("475fe763-e667-467f-b373-bcdfc2e1deab", recurring => recurring.ExecuteAsync(), "*/5 * * * *");
RecurringJob.AddOrUpdate<ScheduledNotificationSendingRecurringJob>("5a27bb09-62d6-4928-bff8-58bac25e69f6", recurring => recurring.ExecuteAsync(), "*/5 * * * *");

await app.StartProjectAsync();