var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.AddSwagger();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwaggerDoc();
app.UseReDoc();
app.UseAuthorization();

app.MapControllers();

app.Run();
