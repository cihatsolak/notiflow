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

app.UseApplicationLifetimes();

app.MapControllers();

app.Run();
