var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services.AddDataDependencies();
builder.Services.AddServiceDependencies();
builder.Services.AddWebDependencies();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuth();
app.UseSwaggerWithRedoclyDoc();
app.UseMigrations();
app.UseApiExceptionHandler();
app.UseResponseCompress();
app.UseHttpSecurity();

app.UseApplicationLifetimes();

app.MapControllers();

await app.StartProjectAsync();
