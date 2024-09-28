using Notiflow.IdentityServer.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.HostConfigured();

builder.AddWebDependencies();

builder.Services
   .AddServiceDependencies(builder.Configuration)
   .AddDataDependencies(builder.Configuration);

builder.AddConfigureHealthChecks();

// Configure the HTTP request pipeline.
var app = builder.Build();

bool isProduction = app.Environment.IsProduction();
if (isProduction)
{
    app.UseHttpSecurityPrecautions();
}
else
{
    app.UseSwaggerRedocly()
       .UseMigrations();
}

app.UseApiExceptionHandler()
   .UseAuth()
   .UseResponseCompression()
   .UseSerilogLogging()
   .UseCustomHttpLogging()
   .UseHealth();

app.UseApplicationLifetimes();

app.UseCihatMiddleware();

app.MapControllers();

await app.StartProjectAsync();