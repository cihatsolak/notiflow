namespace Puzzle.Lib.File.Services;

public sealed class FtpManager : IFileService
{
    private readonly AsyncFtpClient _asyncFtpClient;
    private readonly ILogger<FtpManager> _logger;

    public FtpManager(
        AsyncFtpClient asyncFtpClient,
        ILogger<FtpManager> logger)
    {
        _asyncFtpClient = asyncFtpClient;
        _logger = logger;
    }

    public async Task<bool> AddOrUpdateAsync(byte[] fileData, string directory, string fileNameWithExtension, CancellationToken cancellationToken)
    {
        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(fileData, $"{directory}/{fileNameWithExtension}", createRemoteDir: true, token: cancellationToken);
        bool isFileTransferred = ftpStatus == FtpStatus.Success;
        if (isFileTransferred)
        {
            _logger.LogWarning("-- '{@directory}' -- adresine dosya kaydedilemedi.", directory);
        }

        return isFileTransferred;
    }

    public async Task<bool> AddOrUpdateAsync(IFormFile formFile, string directory, CancellationToken cancellationToken)
    {
        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        return await AddOrUpdateAsync(memoryStream.ToArray(), directory, formFile.FileName, cancellationToken);
    }

    public async Task DeleteAsync(string path, CancellationToken cancellationToken)
    {
        await _asyncFtpClient.DeleteFile(path, cancellationToken);
    }   
}