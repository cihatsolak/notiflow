namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateBlocking;

public sealed class UpdateCustomerBlockingCommandValidator : AbstractValidator<UpdateCustomerBlockingCommand>
{
    public UpdateCustomerBlockingCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
    }
}
