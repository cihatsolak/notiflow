namespace Notiflow.Backoffice.Application.Pipelines;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next().ConfigureAwait(false);
        }

        var validationContext = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(validationContext, cancellationToken))).ConfigureAwait(false);

        var failures = validationResults
                   .Where(validationFailure => validationFailure is not null && validationFailure.Errors.Any())
                   .SelectMany(validationResult => validationResult.Errors);


        _logger.LogInformation("--- Validating command {CommandType}", request.GetTypeName());

        if (failures.Any())
        {
            _logger.LogWarning("Validation errors - {CommandType} - Command: {Command} - Errors: {@ValidationErrors}", typeof(TRequest).Name, request, failures);
            throw new ValidationException(failures);
        }

        return await next().ConfigureAwait(false);
    }
}
