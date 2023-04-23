using Notiflow.IdentityServer.Data;
using Notiflow.IdentityServer.Service;
using Puzzle.Lib.Database.Middlewares;
using Puzzle.Lib.Auth.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtAuthentication();
builder.Services.AddControllers();
builder.Services.AddData();
builder.Services.AddService();





var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMigrations();

app.MapControllers();

app.Run();
