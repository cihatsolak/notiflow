namespace Puzzle.Lib.File.Services;

/// <summary>
/// Defines a set of methods for handling file operations such as adding, updating, and deleting files.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Adds a file if it does not already exist at the specified path.
    /// </summary>
    /// <param name="fileData">The file data as a byte array.</param>
    /// <param name="path">The path where the file should be added.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="FileResult"/> representing the result of the operation.</returns>
    Task<FileResult> AddIfNoExistsAsync(byte[] fileData, string path, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a file from a form file if it does not already exist at the specified path.
    /// </summary>
    /// <param name="formFile">The form file to add.</param>
    /// <param name="path">The path where the file should be added.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="FileResult"/> representing the result of the operation.</returns>
    Task<FileResult> AddIfNoExistsAsync(IFormFile formFile, string path, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a file after renaming it if a file with the same name already exists in the directory.
    /// </summary>
    /// <param name="fileData">The file data as a byte array.</param>
    /// <param name="directory">The directory where the file should be added.</param>
    /// <param name="fileName">The desired file name (without extension).</param>
    /// <param name="extension">The file extension (with or without a dot).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="FileResult"/> representing the result of the operation.</returns>
    Task<FileResult> AddAfterRenameIfAvailableAsync(byte[] fileData, string directory, string fileName, string extension, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a file from a form file after renaming it if a file with the same name already exists in the directory.
    /// </summary>
    /// <param name="formFile">The form file to add.</param>
    /// <param name="directory">The directory where the file should be added.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="FileResult"/> representing the result of the operation.</returns>
    Task<FileResult> AddAfterRenameIfAvailableAsync(IFormFile formFile, string directory, CancellationToken cancellationToken);

    /// <summary>
    /// Adds or updates a file at the specified path.
    /// </summary>
    /// <param name="fileData">The file data as a byte array.</param>
    /// <param name="path">The path where the file should be added or updated.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="FileResult"/> representing the result of the operation.</returns>
    Task<FileResult> AddOrUpdateAsync(byte[] fileData, string path, CancellationToken cancellationToken);

    /// <summary>
    /// Adds or updates a file from a form file at the specified path.
    /// </summary>
    /// <param name="formFile">The form file to add or update.</param>
    /// <param name="path">The path where the file should be added or updated.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="FileResult"/> representing the result of the operation.</returns>
    Task<FileResult> AddOrUpdateAsync(IFormFile formFile, string path, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a file at the specified path.
    /// </summary>
    /// <param name="path">The path of the file to delete.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    Task DeleteAsync(string path, CancellationToken cancellationToken);
}
