namespace Puzzle.Lib.Security.Services.MicrosoftProtectors;

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

    public TData Decrypt<TData>(string protectedData)
    {
        ArgumentException.ThrowIfNullOrEmpty(protectedData);

        return JsonSerializer.Deserialize<TData>(_dataProtector.Unprotect(protectedData));
    }

    public string TimeDependentEncrypt<TData>(TData flatData, int minutesToExpire)
    {
        ArgumentNullException.ThrowIfNull(flatData);

        if (0 >= minutesToExpire)
        {
            throw new ArgumentException("Invalid expiration date information.");
        }

        return _timeLimitedDataProtector.Protect(JsonSerializer.Serialize(flatData), TimeSpan.FromMinutes(minutesToExpire));
    }

    public string TimeDependentEncrypt<TData>(TData flatData, TimeSpan lifetime)
    {
        ArgumentNullException.ThrowIfNull(flatData);

        if (lifetime <= TimeSpan.Zero)
        {
            throw new ArgumentException("Invalid expiration date information.");
        }

        return _timeLimitedDataProtector.Protect(JsonSerializer.Serialize(flatData), lifetime);
    }

    public TData TimeDependentDecrypt<TData>(string protectedData)
    {
        ArgumentException.ThrowIfNullOrEmpty(protectedData);

        return JsonSerializer.Deserialize<TData>(_timeLimitedDataProtector.Unprotect(protectedData));
    }
}