using Notiflow.IdentityServer.Data;
using Notiflow.IdentityServer.Service;
using Puzzle.Lib.Database.Middlewares;
using Puzzle.Lib.Auth.IOC;
using Puzzle.Lib.Auth.Middlewares;
using Puzzle.Lib.Documentation.Middlewares;
using Puzzle.Lib.Documentation.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtAuthentication();
builder.Services.AddControllers();
builder.Services.AddData();
builder.Services.AddSwagger();
builder.Services.AddService();





var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuth();
app.UseSwaggerWithRedoclyDoc();
app.UseMigrations();

app.MapControllers();

app.Run();
