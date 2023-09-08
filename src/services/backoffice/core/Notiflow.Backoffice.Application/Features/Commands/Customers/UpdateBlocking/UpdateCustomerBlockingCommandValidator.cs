namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateBlocking;

public sealed class UpdateCustomerBlockingCommandValidator : AbstractValidator<UpdateCustomerBlockingCommand>
{
    public UpdateCustomerBlockingCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentValidationErrorCodes.ID_NUMBER);
    }
}
