namespace Puzzle.Lib.File.Services;

public sealed class AzureBlobStorageManager : IFileService
{
    public Task<bool> AddOrUpdateAsync(byte[] fileData, string directory, string fileNameWithExtension, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddOrUpdateAsync(IFormFile formFile, string directory, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}