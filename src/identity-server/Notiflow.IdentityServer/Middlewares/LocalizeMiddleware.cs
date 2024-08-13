using Microsoft.IO;
using System.Text;
using System.Text.Json;

namespace Notiflow.IdentityServer.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LocalizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public LocalizeMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await AddResponseBodyContentToActivityTags(context);
        }

        private async Task AddResponseBodyContentToActivityTags(HttpContext context)
        {
            var originalResponse = context.Response.Body;

            await using var responseBodyMemoryStream = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBodyMemoryStream;

            await _next(context);

            responseBodyMemoryStream.Position = 0;

            var responseBodyStreamReader = new StreamReader(responseBodyMemoryStream);
            var responseBodyContent = await responseBodyStreamReader.ReadToEndAsync();

            // Dönüş modelini parse et
            var responseObject = JsonSerializer.Deserialize<Result<object>>(responseBodyContent);

            // Message özelliğini güncelle
            if (responseObject != null)
            {
                responseObject.Message = "Yeni Mesaj"; // Yeni mesajı buraya ekleyin
            }

            // Güncellenmiş dönüş modelini JSON olarak geri yaz
            var updatedResponseBody = JsonSerializer.Serialize(responseObject);

            responseBodyMemoryStream.Position = 0;
            await responseBodyMemoryStream.WriteAsync(Encoding.UTF8.GetBytes(updatedResponseBody).AsMemory(0, Encoding.UTF8.GetBytes(updatedResponseBody).Length));

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
