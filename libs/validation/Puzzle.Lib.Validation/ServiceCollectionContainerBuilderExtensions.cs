namespace Puzzle.Lib.Validation;

/// <summary>
/// Provides extension methods for IServiceCollection that simplify adding FluentValidation and configuring API behavior options.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Configures the service collection to enable Fluent Design validation using the validators from the calling assembly, auto validation, client-side adapters, and sets the language manager culture to the current culture.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddFluentDesignValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.CurrentCulture;

        return services;
    }

    /// <summary>
    /// Configures API behavior options to suppress the inferrence of binding sources for parameters and log validation errors.
    /// </summary>
    /// <param name="services">The service collection to configure the API behavior options on.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApiBehaviorOptions(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressInferBindingSourcesForParameters = true;

            options.InvalidModelStateResponseFactory = context =>
            {
                var asd = context.ModelState.Values.Where(p => p.Errors.Any()).SelectMany(p => p.Errors);
                IEnumerable<string> errors = context.ModelState.Values.Where(p => p.Errors.Any()).SelectMany(p => p.Errors).Select(p => p.ErrorMessage);

                Log.Warning("-- Validation Error. ErrorCodes: {@errors} --", string.Join(",", errors));

                ValidationResponse validationResponse = new()
                {
                    Code = 9004,
                    Message = errors.First(),
                    Errors = errors
                };

                return new BadRequestObjectResult(validationResponse);
            };
        });

        return services;
    }
}