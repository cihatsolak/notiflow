namespace Puzzle.Lib.File.Services;

public interface IFileService
{
    Task<FileProcessResult> AddIfNoExistsAsync(byte[] fileData, string path, CancellationToken cancellationToken);
    Task<FileProcessResult> AddIfNoExistsAsync(IFormFile formFile, string path, CancellationToken cancellationToken);
    Task<FileProcessResult> AddAfterRenameIfAvailableAsync(byte[] fileData, string directory, string fileName, string extention, CancellationToken cancellationToken);
    Task<FileProcessResult> AddAfterRenameIfAvailableAsync(IFormFile formFile, string directory, CancellationToken cancellationToken);
    Task<FileProcessResult> AddOrUpdateAsync(byte[] fileData, string path, CancellationToken cancellationToken);
    Task<FileProcessResult> AddOrUpdateAsync(IFormFile formFile, string path, CancellationToken cancellationToken);
    Task DeleteAsync(string path, CancellationToken cancellationToken);
}