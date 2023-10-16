var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

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