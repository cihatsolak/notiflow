namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record DeleteCustomerCommand(int Id) : IRequest<Result>;

public sealed class DeleteCustomerCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<DeleteCustomerCommandHandler> logger) : IRequestHandler<DeleteCustomerCommand, Result>
{
    
    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await uow.CustomerWrite.ExecuteDeleteByIdAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return Result.Status404NotFound(ResultCodes.CUSTOMER_NOT_DELETED);
        }

        logger.LogInformation("Customer deleted. ID: {customerId}", request.Id);

        return Result.Status204NoContent();
    }
}

public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentVld.Errors.ID_NUMBER);
    }
}
