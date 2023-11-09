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

builder.AddConfigureHealthChecks();

var app = builder.Build();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseHealthAndUIConfiguration();

await app.UseOcelot();

await app.RunAsync(CancellationToken.None);
