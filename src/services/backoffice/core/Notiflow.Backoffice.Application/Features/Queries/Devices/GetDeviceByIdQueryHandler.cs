namespace Notiflow.Backoffice.Application.Features.Queries.Devices;

public sealed record GetDeviceByIdQuery(int Id) : IRequest<Result<GetDeviceByIdQueryResult>>;

public sealed class GetDeviceByIdQueryHandler(INotiflowUnitOfWork uow) : IRequestHandler<GetDeviceByIdQuery, Result<GetDeviceByIdQueryResult>>
{
    public async Task<Result<GetDeviceByIdQueryResult>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<GetDeviceByIdQueryResult>.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        var deviceDto = ObjectMapper.Mapper.Map<GetDeviceByIdQueryResult>(device);

        return Result<GetDeviceByIdQueryResult>.Status200OK(ResultCodes.GENERAL_SUCCESS, deviceDto);
    }
}

public sealed class GetDeviceByIdQueryValidator : AbstractValidator<GetDeviceByIdQuery>
{
    public GetDeviceByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}

public sealed record GetDeviceByIdQueryResult
{
    public int CustomerId { get; set; }
    public OSVersion OSVersion { get; init; }
    public string Code { get; init; }
    public string Token { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
}
