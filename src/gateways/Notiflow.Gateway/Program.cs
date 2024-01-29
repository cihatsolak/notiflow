var builder = WebApplication.CreateBuilder(args);

string environmentName = builder.Environment.EnvironmentName;

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .AddJsonFile($"ocelot.{environmentName}.json", true, true)
    .AddJsonFile($"ocelot.global.json", true, true)
    .AddJsonFile($"ocelot.{environmentName}.SwaggerEndpoint.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();

JwtTokenSetting jwtTokenSetting = builder.Configuration.GetRequiredSection(nameof(JwtTokenSetting)).Get<JwtTokenSetting>();
builder.Services.AddJwtAuthentication(options =>
{
    options.Audiences = jwtTokenSetting.Audiences;
    options.Issuer = jwtTokenSetting.Issuer;
    options.AccessTokenExpirationMinute = jwtTokenSetting.AccessTokenExpirationMinute;
    options.RefreshTokenExpirationMinute = jwtTokenSetting.RefreshTokenExpirationMinute;
    options.SecurityKey = jwtTokenSetting.SecurityKey;
});

builder.Services.AddAuthorization();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.AddConfigureHealthChecks();

var app = builder.Build();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";

}, uiOptions =>
{
    uiOptions.DocumentTitle = "Gateway documentation";
});

app.UseHealthWithUI();
app.UseAuth();

await app.UseOcelot();
app.MapControllers();

await app.RunAsync(CancellationToken.None);
