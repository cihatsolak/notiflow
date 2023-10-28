var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

SeriLogElasticSetting seriLogElasticSetting = builder.Configuration.GetRequiredSection(nameof(SeriLogElasticSetting)).Get<SeriLogElasticSetting>();

builder.Host.AddSeriLogWithElasticSearch(options =>
{
    options.Address = seriLogElasticSetting.Address;
    options.Username = seriLogElasticSetting.Username;
    options.Password = seriLogElasticSetting.Password;
    options.IsRequiredAuthentication = seriLogElasticSetting.IsRequiredAuthentication;
});

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
