﻿namespace Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

public sealed class GetDetailByIdQueryValidator : AbstractValidator<GetDetailByIdQueryRequest>
{
    public GetDetailByIdQueryValidator()
    {
        RuleFor(p => p.Id).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
    }
}