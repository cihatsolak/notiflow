namespace Notiflow.Backoffice.Application.Features.Commands.Devices.DataTable;

public sealed record DeviceDataTableResult
{
    public required int Id { get; init; }
    public OSVersion OSVersion { get; init; }
    public string Code { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
    public DateTime CreatedDate { get; init; }
    public string FullName { get; init; }
}
