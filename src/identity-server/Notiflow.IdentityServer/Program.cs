using Notiflow.IdentityServer.Data;
using Notiflow.IdentityServer.Service;
using Puzzle.Lib.Database.Middlewares;
using Puzzle.Lib.Auth.IOC;
using Puzzle.Lib.Auth.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtAuthentication();
builder.Services.AddControllers();
builder.Services.AddData();
builder.Services.AddService();





var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuth();

app.UseMigrations();

app.MapControllers();

app.Run();
