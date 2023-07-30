using Notiflow.Common.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwagger();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence();

var app = builder.Build();
TenantCacheKeyGeneratorCihat.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwaggerWithRedoclyDoc();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ApplicationIdMiddleware>();
app.UseRequestLocalization();

app.MapControllers();

app.Run();
