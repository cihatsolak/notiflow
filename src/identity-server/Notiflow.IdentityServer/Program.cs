var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services
   .AddWebDependencies(builder.Configuration)
   .AddServiceDependencies(builder.Configuration)
   .AddDataDependencies(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

app
   .UseHttpSecurityPrecautions()
   .UseAuth()
   .UseSwaggerWithRedoclyDoc(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseApiExceptionHandler()
   .UseResponseCompress()
   .UseHealthChecksConfiguration();

app.UseApplicationLifetimes();

app.MapControllers();

await app.StartProjectAsync();
