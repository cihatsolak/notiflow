namespace Puzzle.Lib.Security.Services.Protectors
{
    public sealed class ProtectorManager : IProtectorService
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
            return _dataProtector.Protect(JsonSerializer.Serialize(flatData));
        }

        public TData Decrypt<TData>(string cipherText)
        {
            return JsonSerializer.Deserialize<TData>(_dataProtector.Unprotect(cipherText));
        }

        public string TimeDependentEncrypt<TData>(string flatData, int minute)
        {
            return _timeLimitedDataProtector.Protect(flatData, TimeSpan.FromMinutes(minute));
        }

        public TData TimeDependentDecrypt<TData>(string cipherText)
        {
            return JsonSerializer.Deserialize<TData>(_timeLimitedDataProtector.Unprotect(cipherText));
        }
    }
}