using Notiflow.IdentityServer.Infrastructure.Entities;
using System;

namespace Notiflow.IdentityServer.Fundamentals
{
    public static class IdentityContainerBuilderExtensions
    {
        public IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddPasswordValidator<CustomPasswordValidator>()
            .AddUserValidator<CustomUserValidator>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
