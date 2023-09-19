namespace Puzzle.Lib.Security.Services.Protectors;

internal sealed class ProtectorManager : IProtectorService
{
    private readonly IDataProtector _dataProtector;
    private readonly ITimeLimitedDataProtector _timeLimitedDataProtector;

    public ProtectorManager(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtector = dataProtectionProvider.CreateProtector(GetType().FullName);
        _timeLimitedDataProtector = dataProtectionProvider.CreateProtector(GetType().FullName).ToTimeLimitedDataProtector();
    }

    public string Encrypt<TData>(TData flatData)
    {
        ArgumentNullException.ThrowIfNull(flatData);

        return _dataProtector.Protect(JsonSerializer.Serialize(flatData));
    }

    public TData Decrypt<TData>(string cipherText)
    {
        ArgumentException.ThrowIfNullOrEmpty(cipherText);

        return JsonSerializer.Deserialize<TData>(_dataProtector.Unprotect(cipherText));
    }

    public string TimeDependentEncrypt<TData>(TData flatData, int minutesToExpire)
    {
        ArgumentNullException.ThrowIfNull(flatData);
        SecurityArgumentException.ThrowIfNegativeNumber(minutesToExpire);

        return _timeLimitedDataProtector.Protect(JsonSerializer.Serialize(flatData), TimeSpan.FromMinutes(minutesToExpire));
    }

    public TData TimeDependentDecrypt<TData>(string cipherText)
    {
        ArgumentException.ThrowIfNullOrEmpty(cipherText);

        return JsonSerializer.Deserialize<TData>(_timeLimitedDataProtector.Unprotect(cipherText));
    }
}