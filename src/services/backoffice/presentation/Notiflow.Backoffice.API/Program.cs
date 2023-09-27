var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services
    .AddWebDependencies()
    .AddApplication()
    .AddInfrastructure()
    .AddPersistence();

// Configure the HTTP request pipeline.
var app = builder.Build();

TenantCacheKeyFactory.Configure(app);

app.UseHttpSecurityPrecautions()
   .UseAuth()
   .UseSwaggerWithRedoclyDoc()
   .UseMigrations()
   .UseResponseCompress()
   .UseRequestLocalization()
   .UseHealthChecksConfiguration();

app.UseMiddleware<ApplicationIdMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

await app.StartProjectAsync();
