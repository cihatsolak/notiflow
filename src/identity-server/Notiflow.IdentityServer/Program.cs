var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services
   .AddWebDependencies()
   .AddServiceDependencies()
   .AddDataDependencies(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

app
   .UseHttpSecurityPrecautions()
   .UseAuth()
   .UseSwaggerWithRedoclyDoc()
   .UseMigrations(builder.Environment)
   .UseApiExceptionHandler()
   .UseResponseCompress()
   .UseHealthChecksConfiguration();

app.UseApplicationLifetimes();

app.MapControllers();

await app.StartProjectAsync();
