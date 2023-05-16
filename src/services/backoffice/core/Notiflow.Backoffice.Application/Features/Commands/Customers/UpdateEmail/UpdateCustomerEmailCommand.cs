﻿namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed record UpdateCustomerEmailCommand : IRequest<Response<EmptyResponse>>
{
    public required int Id { get; init; }
    public required string Email { get; init; }
}
