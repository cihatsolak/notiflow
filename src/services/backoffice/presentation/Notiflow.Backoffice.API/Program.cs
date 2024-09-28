var builder = WebApplication.CreateBuilder(args);

builder.Host.Configured(builder.Configuration);

builder.AddWebDependencies();
builder.AddBackofficeHealthChecks();

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();
TenantCacheKeyFactory.Configure(app);

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

app
   .UseAuth()
   .UseResponseCompression()
   .UseSerilogLogging()
   .UseCustomHttpLogging()
   .UseHealth();

app.MapControllers();
app.MapHubs();

await app.StartProjectAsync();
