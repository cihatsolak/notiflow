using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Puzzle.Lib.Session.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add session service
        /// </summary>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0"/>
        /// <param name="services">type of built-in service collection</param>
        /// <param name="idleTimeoutMinute">idle time out minute</param>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddSessionService(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ISessionService, SessionManager>();

            return services;
        }
    }
}