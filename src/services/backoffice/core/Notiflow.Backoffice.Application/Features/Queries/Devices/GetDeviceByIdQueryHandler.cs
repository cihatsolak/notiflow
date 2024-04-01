namespace Notiflow.Backoffice.Application.Features.Queries.Devices;

public sealed record GetDeviceByIdQuery(int Id) : IRequest<Result<DeviceResponse>>;

public sealed class GetDeviceByIdQueryHandler(INotiflowUnitOfWork uow) 
    : IRequestHandler<GetDeviceByIdQuery, Result<DeviceResponse>>
{
    public async Task<Result<DeviceResponse>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<DeviceResponse>.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        return Result<DeviceResponse>.Status200OK(ResultCodes.GENERAL_SUCCESS, ObjectMapper.Mapper.Map<DeviceResponse>(device));
    }
}

public sealed class GetDeviceByIdQueryValidator : AbstractValidator<GetDeviceByIdQuery>
{
    public GetDeviceByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}

public sealed record DeviceResponse
{
    public int CustomerId { get; set; }
    public OSVersion OSVersion { get; init; }
    public string Code { get; init; }
    public string Token { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
}
