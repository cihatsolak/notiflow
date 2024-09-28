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
    public static IServiceCollection AddApiBehaviorWithValidationLogging(this IServiceCollection services, Action<ApiBehaviorSetting> configure)
    {
        ApiBehaviorSetting apiBehaviorSetting = new();
        configure.Invoke(apiBehaviorSetting);

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressInferBindingSourcesForParameters = apiBehaviorSetting.SuppressInferBindingSourcesForParameters;
            options.SuppressModelStateInvalidFilter = apiBehaviorSetting.SuppressModelStateInvalidFilter;

            options.InvalidModelStateResponseFactory = context =>
            {
                var errorMessages = context.ModelState.Values
                    .Where(validation => validation.Errors.Any())
                    .SelectMany(validation => validation.Errors)
                    .Select(error => error.ErrorMessage);

                ILogger logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger(nameof(FluentValidation));

                logger.LogWarning("FluentValidation Error: {Errors}", string.Join(",", errorMessages, CultureInfo.InvariantCulture));

                return new BadRequestObjectResult(apiBehaviorSetting.IsProductionEnvironment
                    ? new { Message = apiBehaviorSetting.ErrorMessage, Errors = errorMessages }
                    : new { Message = apiBehaviorSetting.ErrorMessage });
            };
        });

        return services;
    }

}