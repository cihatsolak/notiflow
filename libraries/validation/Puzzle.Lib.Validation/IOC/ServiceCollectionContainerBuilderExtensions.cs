namespace Puzzle.Lib.Validation.IOC
{
    /// <summary>
    /// Provides extension methods for IServiceCollection that simplify adding FluentValidation and configuring API behavior options.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Registers validators from the assembly containing TAssembyScanner with the provided service collection and adds FluentValidation auto-validation and client-side adapters.
        /// </summary>
        /// <typeparam name="TAssembyScanner">The type to be scanned for validators.</typeparam>
        /// <param name="services">The service collection to add the validators and related services to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddFluentValidation<TAssembyScanner>(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<TAssembyScanner>();
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
                    IEnumerable<string> errors = context.ModelState.Values.Where(p => p.Errors.Any()).SelectMany(p => p.Errors).Select(p => p.ErrorMessage);

                    Log.Warning("-- Validation Error. ErrorCodes: {@errors} --", string.Join(",", errors));

                    ValidationResponse validationResponse = new()
                    {
                        StatusCode = 9004,
                        StatusMessage = errors.First(),
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationResponse);
                };
            });

            return services;
        }
    }
}