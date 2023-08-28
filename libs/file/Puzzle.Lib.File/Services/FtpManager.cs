namespace Puzzle.Lib.File.Services;

public sealed class FtpManager : IFileService
{
    private readonly AsyncFtpClient _asyncFtpClient;
    private readonly FtpSetting _ftpSetting;
    private readonly ILogger<FtpManager> _logger;

    public FtpManager(
        AsyncFtpClient asyncFtpClient,
        IOptions<FtpSetting> ftpSetting,
        ILogger<FtpManager> logger)
    {
        _asyncFtpClient = asyncFtpClient;
        _ftpSetting = ftpSetting.Value;
        _logger = logger;
    }

    public async Task<FileProcessResult> AddIfNoExistsAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        bool isExists = await _asyncFtpClient.FileExists(path, cancellationToken);
        if (isExists)
        {
            _logger.LogWarning("-- '{@path}' -- the file you want to save in path exists.", path);
            return FileProcessResult.Fail();
        }

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(fileData, path, FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- '{@path}' -- Could not save file to path.", path);
            return FileProcessResult.Fail();
        }

        return FileProcessResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileProcessResult> AddIfNoExistsAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        bool isExists = await _asyncFtpClient.FileExists($"{path}/{formFile.FileName}", cancellationToken);
        if (isExists)
        {
            _logger.LogWarning("-- '{@path}' -- the file you want to save in path exists.", path);
            return FileProcessResult.Fail();
        }

        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(memoryStream.ToArray(), $"{path}/{formFile.FileName}", FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- '{@path}' -- Could not save file to path.", path);
            return FileProcessResult.Fail();
        }

        return FileProcessResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileProcessResult> AddAfterRenameIfAvailableAsync(byte[] fileData, string directory, string fileName, string extention, CancellationToken cancellationToken)
    {
        string path = await GenerateUniqueFileNameAsync(directory, fileName, extention, cancellationToken);
        if (string.IsNullOrEmpty(path))
        {
            _logger.LogWarning("loglama");
            return FileProcessResult.Fail();
        }

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(fileData, path, FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- '{@path}' -- Could not save file to path.", path);
            return FileProcessResult.Fail();
        }

        return FileProcessResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileProcessResult> AddAfterRenameIfAvailableAsync(IFormFile formFile, string directory, CancellationToken cancellationToken)
    {
        string path = await GenerateUniqueFileNameAsync(directory, Path.GetFileNameWithoutExtension(formFile.FileName), Path.GetExtension(formFile.FileName), cancellationToken);
        if (string.IsNullOrEmpty(path))
        {
            _logger.LogWarning("loglama"); //todo
            return FileProcessResult.Fail();
        }

        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(memoryStream.ToArray(), path, FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- '{@path}' -- Could not save file to path.", path);
            return FileProcessResult.Fail();
        }

        return FileProcessResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileProcessResult> AddOrUpdateAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(fileData, path, createRemoteDir: true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- Could not save file to '{@path}' --.", path);
            return FileProcessResult.Fail();
        }

        return FileProcessResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileProcessResult> AddOrUpdateAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        return await AddOrUpdateAsync(memoryStream.ToArray(), $"{path}/{formFile.FileName}", cancellationToken);
    }

    public async Task DeleteAsync(string path, CancellationToken cancellationToken)
    {
        await _asyncFtpClient.DeleteFile(path, cancellationToken);
    }

    private async Task<string> GenerateUniqueFileNameAsync(string directory, string fileName, string extention, CancellationToken cancellationToken)
    {
        string path = $"{directory}/{fileName}{extention}";

        bool isExists = await _asyncFtpClient.FileExists(path, cancellationToken);
        if (!isExists)
            return path;

        for (int index = 1; index < int.MaxValue; index++)
        {
            path = $"{directory}/{fileName}-{index}{extention}";

            isExists = await _asyncFtpClient.FileExists(path, cancellationToken);
            if (!isExists)
            {
                break;
            }
        }

        return path;
    }
}