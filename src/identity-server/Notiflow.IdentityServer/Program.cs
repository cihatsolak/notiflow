var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();





var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
