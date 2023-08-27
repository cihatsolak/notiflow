namespace Puzzle.Lib.File.Services;

public interface IFileService
{
    Task<bool> AddOrUpdateAsync(byte[] fileData, string directory, string fileNameWithExtension, CancellationToken cancellationToken);
    Task<bool> AddOrUpdateAsync(IFormFile formFile, string directory, CancellationToken cancellationToken);
    Task DeleteAsync(string path, CancellationToken cancellationToken);
}