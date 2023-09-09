using Notiflow.Backoffice.Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
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
   .UseRequestLocalization();

app.UseMiddleware<ApplicationIdMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

await app.StartProjectAsync();
