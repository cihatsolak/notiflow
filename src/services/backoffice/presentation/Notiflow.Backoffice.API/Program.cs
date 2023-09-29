var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services
    .AddWebDependencies(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure()
    .AddPersistence(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

TenantCacheKeyFactory.Configure(app);

app.UseHttpSecurityPrecautions()
   .UseAuth()
   .UseSwaggerWithRedoclyDoc(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseResponseCompress()
   .UseRequestLocalization()
   .UseHealthChecksConfiguration();

app.UseMiddleware<ApplicationIdMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

await app.StartProjectAsync();
