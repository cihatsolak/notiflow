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

    public async Task<FileResult> AddIfNoExistsAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        CheckArguments(fileData, path);

        bool isExists = await _asyncFtpClient.FileExists(path, cancellationToken);
        if (isExists)
        {
            _logger.LogWarning("-- {path} -- the file you want to save in path exists.", path);
            return FileResult.Fail();
        }

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(fileData, path, FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- {path} -- Could not save file to path.", path);
            return FileResult.Fail();
        }

        return FileResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileResult> AddIfNoExistsAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        CheckArguments(formFile, path);

        string fileName = FileExtensions.CharacterRegulatory(formFile.FileName);

        bool isExists = await _asyncFtpClient.FileExists($"{path}/{fileName}", cancellationToken);
        if (isExists)
        {
            _logger.LogWarning("-- {path} -- the file you want to save in path exists.", path);
            return FileResult.Fail();
        }

        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(memoryStream.ToArray(), $"{path}/{fileName}", FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- {path} -- Could not save file to path.", path);
            return FileResult.Fail();
        }

        return FileResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileResult> AddAfterRenameIfAvailableAsync(byte[] fileData, string directory, string fileName, string extention, CancellationToken cancellationToken)
    {
        CheckArguments(fileData, directory, fileName, extention);

        fileName = FileExtensions.CharacterRegulatory(fileName);
        string path = await GenerateUniqueFileNameAsync(directory, fileName, extention, cancellationToken);

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(fileData, path, FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- {path} -- Could not save file to path.", path);
            return FileResult.Fail();
        }

        return FileResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileResult> AddAfterRenameIfAvailableAsync(IFormFile formFile, string directory, CancellationToken cancellationToken)
    {
        string fileName = FileExtensions.CharacterRegulatory(Path.GetFileNameWithoutExtension(formFile.FileName));
        string path = await GenerateUniqueFileNameAsync(directory, fileName, Path.GetExtension(formFile.FileName), cancellationToken);

        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(memoryStream.ToArray(), path, FtpRemoteExists.Skip, true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- {path} -- Could not save file to path.", path);
            return FileResult.Fail();
        }

        return FileResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileResult> AddOrUpdateAsync(byte[] fileData, string path, CancellationToken cancellationToken)
    {
        CheckArguments(fileData, path);

        FtpStatus ftpStatus = await _asyncFtpClient.UploadBytes(fileData, path, createRemoteDir: true, token: cancellationToken);
        if (ftpStatus != FtpStatus.Success)
        {
            _logger.LogWarning("-- {path} -- Could not save file to path.", path);
            return FileResult.Fail();
        }

        return FileResult.Success($"{_ftpSetting.Url}{path}");
    }

    public async Task<FileResult> AddOrUpdateAsync(IFormFile formFile, string path, CancellationToken cancellationToken)
    {
        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        return await AddOrUpdateAsync(memoryStream.ToArray(), $"{path}/{FileExtensions.CharacterRegulatory(formFile.FileName)}", cancellationToken);
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
        {
            return path;
        }

        long index = 0;

        do
        {
            index++;
            path = $"{directory}/{fileName}-{index}{extention}";

        } while (await _asyncFtpClient.FileExists(path, cancellationToken));

        return path;
    }

    private static void CheckArguments(byte[] fileData, string path)
    {
        ArgumentNullException.ThrowIfNull(fileData);
        ArgumentException.ThrowIfNullOrEmpty(path);
    }

    private static void CheckArguments(IFormFile formFile, string path)
    {
        ArgumentNullException.ThrowIfNull(formFile);
        ArgumentException.ThrowIfNullOrEmpty(path);
    }

    private static void CheckArguments(byte[] fileData, string directory, string fileName, string extention)
    {
        ArgumentNullException.ThrowIfNull(fileData);
        ArgumentException.ThrowIfNullOrEmpty(directory);
        ArgumentException.ThrowIfNullOrEmpty(fileName);
        ArgumentException.ThrowIfNullOrEmpty(extention);
    }
}