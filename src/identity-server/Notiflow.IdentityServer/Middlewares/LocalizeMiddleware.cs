using Microsoft.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Notiflow.IdentityServer.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LocalizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        //public LocalizeMiddleware(RequestDelegate next, ILocalizerService<ResultCodes> localizerService)
        //{
        //    _next = next;
        //    _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        //    _localizerService = 
        //    _localizerService = localizerService;
        //}

        public LocalizeMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalResponse = context.Response.Body;

            await using var responseBodyMemoryStream = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBodyMemoryStream;

            await _next(context);

            responseBodyMemoryStream.Position = 0;

            var responseBodyStreamReader = new StreamReader(responseBodyMemoryStream);
            var responseBodyContent = await responseBodyStreamReader.ReadToEndAsync();

            JsonNode jsonNode = JsonNode.Parse(responseBodyContent);

            // 'message' alanını güncelleyin
            //jsonNode["message"] = localizerService[jsonNode["message"]];
            string updatedJsonString = jsonNode.ToJsonString(new JsonSerializerOptions { WriteIndented = true });


            responseBodyMemoryStream.Position = 0;
            await responseBodyMemoryStream.WriteAsync(Encoding.UTF8.GetBytes(updatedJsonString).AsMemory(0, Encoding.UTF8.GetBytes(updatedJsonString).Length));

            // Orjinal yanıt akışına kopyala
            responseBodyMemoryStream.Position = 0; // Position'ı sıfırla
            await responseBodyMemoryStream.CopyToAsync(originalResponse);

            context.Response.Body = originalResponse;
        }
    }


    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LocalizeMiddlewareExtensions
    {
        public static IApplicationBuilder UseCihatMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizeMiddleware>();
        }
    }
}
