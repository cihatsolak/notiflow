namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed class GetDeviceByIdQueryValidator : AbstractValidator<GetDeviceByIdQuery>
{
    public GetDeviceByIdQueryValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
    }
}
