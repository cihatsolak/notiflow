using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Puzzle.Lib.Social.Infrastructure;

namespace Puzzle.Lib.Social;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddSocialSettings(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        services.Configure<SocialSettings>(configuration.GetRequiredSection(nameof(SocialSettings)));

        return services;
    }
}