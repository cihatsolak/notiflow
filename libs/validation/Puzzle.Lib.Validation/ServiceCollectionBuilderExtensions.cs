namespace Puzzle.Lib.Validation;

/// <summary>
/// Provides extension methods for IServiceCollection that simplify adding FluentValidation and configuring API behavior options.
/// </summary>
public static class ServiceCollectionBuilderExtensions
{
    /// <summary>
    /// Adds server-side validation to the IServiceCollection.
    /// </summary>
    /// <param name="services">The service collection to add the server-side validation to.</param>
    /// <param name="configure">The action to configure the FluentValidation settings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddServerSideValidation(this IServiceCollection services, Action<FluentValidationSetting> configure)
    {
        FluentValidationSetting fluentValidationSetting = new();
        configure.Invoke(fluentValidationSetting);

        services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
        services.AddFluentValidationAutoValidation();

        ValidatorOptions.Global.LanguageManager.Culture = fluentValidationSetting.CultureInfo;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = fluentValidationSetting.CascadeMode;

        return services;
    }

    /// <summary>
    /// Adds client-side validation to the IServiceCollection.
    /// </summary>
    /// <param name="services">The service collection to add the client-side validation to.</param>
    /// <param name="configure">The action to configure the FluentValidation settings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddClientSideValidation(this IServiceCollection services, Action<FluentValidationSetting> configure)
    {
        FluentValidationSetting fluentValidationSetting = new();
        configure.Invoke(fluentValidationSetting);

        services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        ValidatorOptions.Global.LanguageManager.Culture = fluentValidationSetting.CultureInfo;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = fluentValidationSetting.CascadeMode;

        return services;
    }

    /// <summary>
    /// Configures API behavior options to suppress the inferrence of binding sources for parameters and log validation errors.
    /// </summary>
    /// <param name="services">The service collection to configure the API behavior options on.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApiBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressInferBindingSourcesForParameters = false;
            options.SuppressModelStateInvalidFilter = false;

            options.InvalidModelStateResponseFactory = context =>
            {
                // ModelState üzerinden hata mesajlarını toplama
                var errorMessages = context.ModelState.Values
                    .Where(v => v.Errors.Any())
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                // Hata mesajını loglama
                var logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("Validation");

                logger.LogWarning("Validation Error: {Errors}", string.Join(", ", errorMessages));

                // İlk hata mesajını içeren yanıt döndürme
                return new BadRequestObjectResult(new
                {
                    Message = errorMessages.FirstOrDefault() ?? "Validation error occurred."
                });
            };
        });

        return services;
    }

}