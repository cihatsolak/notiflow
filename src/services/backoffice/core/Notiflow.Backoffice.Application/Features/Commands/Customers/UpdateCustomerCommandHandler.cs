﻿namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerCommand(
    int Id,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email,
    DateTime BirthDate,
    Gender Gender,
    MarriageStatus MarriageStatus
    )
    : IRequest<Result<Unit>>;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;

    public UpdateCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<UpdateCustomerCommandHandler> logger)
    {
        _uow = uow;;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        ObjectMapper.Mapper.Map(request, customer);

        _uow.CustomerWrite.Update(customer);
        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Customer updated. ID: {customerId}", request.Id);

        return Result<Unit>.Status204NoContent(ResultCodes.CUSTOMER_UPDATED);
    }
}

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private const int CUSTOMER_NAME_MAX_LENGTH = 50;
    private const int CUSTOMER_SURNAME_MAX_LENGTH = 75;

    public UpdateCustomerCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
        RuleFor(p => p.Name).Ensure(localizer[ValidationErrorMessage.CUSTOMER_NAME], CUSTOMER_NAME_MAX_LENGTH);
        RuleFor(p => p.Surname).Ensure(localizer[ValidationErrorMessage.CUSTOMER_SURNAME], CUSTOMER_SURNAME_MAX_LENGTH);
        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ValidationErrorMessage.PHONE_NUMBER]);
        RuleFor(p => p.Email).Email(localizer[ValidationErrorMessage.EMAIL]);
        RuleFor(p => p.BirthDate).BirthDate(localizer[ValidationErrorMessage.BIRTH_DATE]);
        RuleFor(p => p.Gender).Enum(localizer[ValidationErrorMessage.GENDER]);
        RuleFor(p => p.MarriageStatus).Enum(localizer[ValidationErrorMessage.MARRIAGE_STATUS]);
    }
}