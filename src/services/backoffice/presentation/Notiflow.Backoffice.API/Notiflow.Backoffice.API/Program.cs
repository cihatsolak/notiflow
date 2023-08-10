using Notiflow.Backoffice.Application.Filters;
using Notiflow.Common.Extensions;
using Puzzle.Lib.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddJwtAuthentication();

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.Filters.Add<TenantTokenAuthenticationFilter>();
});

builder.Services.AddSwagger();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence();


// Configure the HTTP request pipeline.
var app = builder.Build();

TenantCacheKeyFactory.Configure(app);

app.UseHttpsRedirection();
app.UseSwaggerWithRedoclyDoc();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ApplicationIdMiddleware>();
app.UseRequestLocalization();

app.MapControllers();

app.Run();
