﻿namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed record DeleteCustomerCommand : IRequest<Response<EmptyResponse>>
{
    public required int Id { get; init; }
}