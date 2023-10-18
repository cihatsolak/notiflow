using Notiflow.Schedule;
using Puzzle.Lib.Documentation.Settings;

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

RecurringJob.AddOrUpdate<ScheduledTextMessageSendingRecurringJob>("Beklemede olan mesajlarýn gönderimi.", recurringJob => recurringJob.ExecuteAsync(), "*/5 * * * *");


await app.StartProjectAsync();