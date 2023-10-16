namespace Puzzle.Lib.File.Services;

public sealed class AzureBlobStorageManager : IFileService
{
    public Task<FileResult> AddAfterRenameIfAvailableAsync(byte[] fileData, string directory, string fileName, string extension, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileResult> AddAfterRenameIfAvailableAsync(IFormFile formFile, string directory, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileResult> AddIfNoExistsAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileResult> AddIfNoExistsAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileResult> AddOrUpdateAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileResult> AddOrUpdateAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}