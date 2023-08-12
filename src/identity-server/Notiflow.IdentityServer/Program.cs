var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut();

builder.Services.AddWebDependencies();
builder.Services.AddServiceDependencies();
builder.Services.AddDataDependencies();


var app = builder.Build();

app.UseHttpSecurityPrecautions();
app.UseAuth();
app.UseSwaggerWithRedoclyDoc();
app.UseMigrations();
app.UseApiExceptionHandler();
app.UseResponseCompress();

app.UseApplicationLifetimes();

app.MapControllers();

await app.StartProjectAsync();
