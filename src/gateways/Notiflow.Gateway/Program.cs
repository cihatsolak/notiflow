using Puzzle.Lib.HealthCheck;

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
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI(settings =>
{
    settings.AddHealthCheckEndpoint("Notiflow.Gateway", "https://localhost:7282/health");
    settings.AddHealthCheckEndpoint("Notiflow.IdentityServer", "https://localhost:7006/health");
    settings.AddHealthCheckEndpoint("Notiflow.Backoffice.API", "https://localhost:7139/health");
    //settings.SetEvaluationTimeInSeconds(60);
    //settings.SetApiMaxActiveRequests(150);
    //settings.MaximumHistoryEntriesPerEndpoint(5000);
}).AddInMemoryStorage(databaseName: Guid.NewGuid().ToString());

var app = builder.Build();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseHealthChecksConfiguration();

app.UseHealthChecksUI(setup =>
{
    setup.UIPath = "/health-ui";
});

await app.UseOcelot();

await app.RunAsync();
