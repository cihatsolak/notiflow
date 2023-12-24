﻿namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed record GetDeviceByIdQueryResult
{
    public int CustomerId { get; set; }
    public OSVersion OSVersion { get; init; }
    public string Code { get; init; }
    public string Token { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
}
