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

var app = builder.Build();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

await app.UseOcelot();

app.Run();
