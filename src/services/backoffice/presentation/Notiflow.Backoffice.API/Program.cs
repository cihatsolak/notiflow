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

app.UseHttpSecurityPrecautions(builder.Environment)
   .UseAuth()
   .UseSwaggerWithRedoclyDoc(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseResponseCompress()
   .UseHealthChecksConfiguration();

app.UseLocalizationWithEndpoint();

app.MapControllers();

await app.StartProjectAsync();
