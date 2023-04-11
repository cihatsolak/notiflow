using Puzzle.Lib.Documentation.IOC;
using Puzzle.Lib.Documentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseHttpsRedirection();
app.UseSwaggerWithRedoclyDoc();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
