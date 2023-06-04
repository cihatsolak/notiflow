using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile($"ocelot.global.json", true, true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.SwaggerEndpoint.json", true, true)
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
