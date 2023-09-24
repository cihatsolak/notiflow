var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services
   .AddWebDependencies()
   .AddServiceDependencies()
   .AddDataDependencies();

// Configure the HTTP request pipeline.
var app = builder.Build();

app
   .UseHttpSecurityPrecautions()
   .UseAuth()
   .UseSwaggerWithRedoclyDoc()
   .UseMigrations()
   .UseApiExceptionHandler()
   .UseResponseCompress();

app.UseApplicationLifetimes();

app.MapControllers();

await app.StartProjectAsync();
