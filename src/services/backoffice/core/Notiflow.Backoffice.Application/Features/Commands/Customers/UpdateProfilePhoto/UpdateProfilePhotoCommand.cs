using FluentFTP;
using Microsoft.AspNetCore.Http;
using Puzzle.Lib.File.Services;

namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateProfilePhoto;

public sealed record UpdateProfilePhotoCommand : IRequest<Response<string>>
{
    public required int CustomerId { get; set; }
    public required IFormFile ProfilePhoto { get; set; }
}

public sealed class UpdateProfilePhotoCommandHandler : IRequestHandler<UpdateProfilePhotoCommand, Response<string>>
{
    private readonly IFileService _fileService;

    public UpdateProfilePhotoCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<Response<string>> Handle(UpdateProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        
    }
}
