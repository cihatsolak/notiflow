namespace Puzzle.Lib.File.Services;

public sealed class AzureBlobStorageManager : IFileService
{
    public Task<FileProcessResult> AddAfterRenameIfAvailableAsync(byte[] fileData, string directory, string fileName, string extention, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileProcessResult> AddAfterRenameIfAvailableAsync(IFormFile formFile, string directory, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileProcessResult> AddIfNoExistsAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileProcessResult> AddIfNoExistsAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileProcessResult> AddOrUpdateAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileProcessResult> AddOrUpdateAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}