var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

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

var app = builder.Build();

app.UseHangfire();

await app.StartProjectAsync();