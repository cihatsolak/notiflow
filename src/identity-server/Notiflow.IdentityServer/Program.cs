using Puzzle.Lib.Host.Infrastructure;

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

builder.Services.AddConfigureHealthChecks(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseAuth();

app
   .UseApiExceptionHandler()
   .UseHttpSecurityPrecautions(builder.Environment)
   .UseSwaggerWithRedoclyDoc(builder.Environment)
   .UseMigrations(builder.Environment)
   .UseResponseCompression()
   .UseSerilogLogging()
   .UseCustomHttpLogging()
   .UseHealth();

app.UseLocalizationWithEndpoint();
app.UseApplicationLifetimes();

app.MapControllers();

await app.StartProjectAsync();