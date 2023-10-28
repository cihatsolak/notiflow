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
   .AddServiceDependencies(builder.Configuration)
   .AddDataDependencies(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

app
   .UseHttpSecurityPrecautions(builder.Environment)
   .UseAuth()
   .UseSwaggerWithRedoclyDoc(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseApiExceptionHandler()
   .UseResponseCompress()
   .UseSerilogLogging()
   .UseCustomHttpLogging()
   .UseHealthChecksConfiguration();

app.UseLocalizationWithEndpoint();
app.UseApplicationLifetimes();

app.MapControllers();

await app.StartProjectAsync();