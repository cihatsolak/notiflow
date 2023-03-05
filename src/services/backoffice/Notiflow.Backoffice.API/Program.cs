var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwaggerDoc();
app.UseReDoc();
app.UseAuthorization();

app.MapControllers();

app.Run();


/// <summary>
/// Start project
/// </summary>
/// <param name="app">type of web application</param>
//public static async ValueTask StartProjectAsync(this IHostBuilder hostBuilder)
//{
//    string applicationName = app.Environment.ApplicationName;

//    try
//    {
//        Log.Information("-- Starting web host: {@applicationName} --", applicationName);
//        await hostBuilder.RunAsync();
//    }
//    catch (Exception exception)
//    {
//        Log.Fatal(exception, "-- Host terminated unexpectedly. {@applicationName} -- ", applicationName);
//        await app.StopAsync();
//    }
//    finally
//    {
//        Log.CloseAndFlush();
//        await app.DisposeAsync();
//    }
//}