namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record DeleteCustomerCommand(int Id) : IRequest<Result<EmptyResponse>>;

public sealed class DeleteCustomerCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<DeleteCustomerCommandHandler> logger) : IRequestHandler<DeleteCustomerCommand, Result<EmptyResponse>>
{
    
    public async Task<Result<EmptyResponse>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await uow.CustomerWrite.ExecuteDeleteByIdAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return Result<EmptyResponse>.Status404NotFound(ResultCodes.CUSTOMER_NOT_DELETED);
        }

        logger.LogInformation("Customer deleted. ID: {customerId}", request.Id);

        return Result<EmptyResponse>.Status204NoContent(ResultCodes.CUSTOMER_DELETED);
    }
}

public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}
