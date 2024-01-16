var builder = WebApplication.CreateBuilder(args);

builder.Host.Configured(builder.Configuration);

builder.Services
    .AddWebDependencies(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

TenantCacheKeyFactory.Configure(app);

app.UseHttpSecurityPrecautions(builder.Environment)
   .UseAuth()
   .UseSwaggerRedocly(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseResponseCompression()
   .UseSerilogLogging()
   .UseCustomHttpLogging()
   .UseHealth();

app.UseLocalizationWithEndpoint();

app.MapControllers();
app.MapHubs();

await app.StartProjectAsync();
