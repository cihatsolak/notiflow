namespace Puzzle.Lib.Cache.Services.Cache;

internal sealed class StackExchangeRedisManager : IRedisService
{
    private readonly IDatabase _database;
    private readonly IServer _server;
    private readonly ILogger<StackExchangeRedisManager> _logger;
    private readonly RedisServerSetting _redisServerSetting;
    private readonly CultureInfo _englishCultureInfo;

    public StackExchangeRedisManager(
        IConnectionMultiplexer connectionMultiplexer,
        IOptions<RedisServerSetting> redisServerSetting,
        ILogger<StackExchangeRedisManager> logger)
    {
        _database = connectionMultiplexer.GetDatabase(redisServerSetting.Value.DefaultDatabase);
        _server = connectionMultiplexer.GetServer(redisServerSetting.Value.ConnectionString);
        _logger = logger;
        _redisServerSetting = redisServerSetting.Value;
        _englishCultureInfo = new CultureInfo("en-US");
    }

    public async Task<bool> ExistsAsync(string cacheKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await _database.KeyExistsAsync(KeyLower(cacheKey), CommandFlags.PreferReplica);
        });
    }

    public async Task<long> IncrementAsync(string cacheKey, int increment)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            long result = await _database.StringIncrementAsync(KeyLower(cacheKey), increment, CommandFlags.DemandMaster);
            if (0 >= result)
            {
                _logger.LogWarning("The key value {cacheKey} could not be incremented by {increment}.", cacheKey, increment);
            }

            return result;
        });
    }

    public async Task<long> DecrementAsync(string cacheKey, int decrement)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            long result = await _database.StringDecrementAsync(KeyLower(cacheKey), decrement, CommandFlags.DemandMaster);
            if (0 >= result)
            {
                _logger.LogWarning("The {cacheKey} key value has been reduced by {increment}.", cacheKey, decrement);
            }

            return result;
        });
    }

    public async Task<bool> HashExistsAsync(string cacheKey, string hashField)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(hashField);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await _database.HashExistsAsync(KeyLower(cacheKey), hashField, CommandFlags.PreferReplica);
        });
    }

    public async Task<IEnumerable<KeyValuePair<string, string>>> HashGetAllAsync(string cacheKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var hashEntries = await _database.HashGetAllAsync(KeyLower(cacheKey), CommandFlags.PreferReplica);
            if (hashEntries.Length == 0)
            {
                _logger.LogWarning("Data for key {cacheKey} not found.", cacheKey);
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            return hashEntries.Select(hashEntry => new KeyValuePair<string, string>(hashEntry.Name, hashEntry.Value));
        });
    }

    public async Task<TData> HashGetAsync<TData>(string cacheKey, string hashField)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(hashField);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var hashEntry = await _database.HashGetAsync(KeyLower(cacheKey), hashField, CommandFlags.PreferReplica);
            if (!hashEntry.HasValue)
            {
                _logger.LogWarning("Data for key {cacheKey} not found.", cacheKey);
                return default;
            }

            return JsonSerializer.Deserialize<TData>(hashEntry);
        });
    }

    public async Task<bool> HashSetAsync<TValue>(string cacheKey, string hashField, TValue value)
    {
        CheckArguments(cacheKey, hashField, value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.HashSetAsync(KeyLower(cacheKey), hashField, JsonSerializer.Serialize(value), When.Always, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("The data for the {cacheKey} key value could not be transferred to the redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> HashDeleteAsync(string cacheKey, string hashField)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(hashField);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.HashDeleteAsync(KeyLower(cacheKey), hashField, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Unable to delete data for key value {cacheKey}.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<int> SortedSetIncrementAsync(string cacheKey, string memberKey, int increment)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(memberKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            double result = await _database.SortedSetIncrementAsync(KeyLower(cacheKey), memberKey, increment, CommandFlags.DemandMaster);
            if (result == 0)
            {
                _logger.LogWarning("Could not update member {@memberKey} in the ordered list of key value {cacheKey}.", cacheKey, memberKey);
            }

            return (int)result;
        });
    }

    public async Task<IEnumerable<TData>> GetSortedListInDescendingOrderOfScoreAsync<TData>(string cacheKey, int start = 0, int stop = -1) where TData : struct
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var redisValues = await _database.SortedSetRangeByRankAsync(KeyLower(cacheKey), start, stop, Order.Descending, CommandFlags.PreferReplica);
            if (redisValues.Length == 0)
            {
                _logger.LogWarning("No data found in the ordered list of key value {cacheKey}. | start: {@start}, stop: {@stop}", cacheKey, start, stop);
                return Enumerable.Empty<TData>();
            }

            return redisValues.Select(redisValue => (TData)Convert.ChangeType(redisValue, typeof(TData)));
        });
    }

    public async Task<IEnumerable<TData>> GetSortedListInAscendingOrderOfScoreAsync<TData>(string cacheKey, int start = 0, int stop = -1) where TData : struct
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var redisValues = await _database.SortedSetRangeByRankAsync(KeyLower(cacheKey), start, stop, Order.Ascending, CommandFlags.PreferReplica);
            if (redisValues.Length == 0)
            {
                _logger.LogWarning("No data found in the ordered list of key value {cacheKey}. | start: {@start}, stop: {@stop}", cacheKey, start, stop);
                return Enumerable.Empty<TData>();
            }

            return redisValues.Select(redisValue => (TData)Convert.ChangeType(redisValue, typeof(TData)));
        });
    }

    public async Task<bool> SortedSetDeleteAsync(string cacheKey, string memberKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(memberKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.SortedSetRemoveAsync(KeyLower(cacheKey), memberKey, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not delete member {@memberKey} in the ordered list of key value {cacheKey}.", cacheKey, memberKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> SetExistsAsync<TValue>(string cacheKey, TValue value)
    {
        CheckArguments(cacheKey, value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await _database.SetContainsAsync(KeyLower(cacheKey), JsonSerializer.Serialize(value), CommandFlags.PreferReplica);
        });
    }

    public async Task<IEnumerable<TData>> SetMembersAsync<TData>(string cacheKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var redisValues = await _database.SetMembersAsync(KeyLower(cacheKey), CommandFlags.PreferReplica);
            if (redisValues.Length == 0)
            {
                _logger.LogWarning("The list of {cacheKey} key could not be found.", cacheKey);
            }

            return redisValues.Select(redisValue => (TData)Convert.ChangeType(redisValue, typeof(TData)));
        });
    }

    public async Task<bool> SetAddAsync<TValue>(string cacheKey, TValue value)
    {
        CheckArguments(cacheKey, value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.SetAddAsync(KeyLower(cacheKey), JsonSerializer.Serialize(value), CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not transfer data {cacheKey} to redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> SetRemoveAsync<TValue>(string cacheKey, TValue value)
    {
        CheckArguments(cacheKey, value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.SetRemoveAsync(KeyLower(cacheKey), JsonSerializer.Serialize(value), CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Unable to delete element in unique list belonging to {cacheKey} key.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<TResponse> StringGetAsync<TResponse>(string cacheKey) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var redisValue = await _database.StringGetAsync(KeyLower(cacheKey), CommandFlags.PreferReplica);
            if (!redisValue.HasValue)
                return default;

            return JsonSerializer.Deserialize<TResponse>(redisValue);
        });
    }

    public async Task<bool> StringSetAsync<TValue>(string cacheKey, TValue value)
    {
        CheckArguments(cacheKey, value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.StringSetAsync(KeyLower(cacheKey), JsonSerializer.Serialize(value), null, When.Always, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not transfer data {cacheKey} to redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> StringSetAsync<TValue>(string cacheKey, TValue value, int cacheDurationInMinutes)
    {
        CheckArguments(cacheKey, value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.StringSetAsync(KeyLower(cacheKey), JsonSerializer.Serialize(value), TimeSpan.FromMinutes(cacheDurationInMinutes), When.Always, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not transfer data {cacheKey} to redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> ChangeStringAsync<TValue>(string cacheKey, TValue value)
    {
        CheckArguments(cacheKey, value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.StringSetAsync(KeyLower(cacheKey), JsonSerializer.Serialize(value), null, true, When.Exists, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not transfer data {cacheKey} to redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> ExtendCacheDurationAsync(string cacheKey, int cacheDurationInMinutes)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            TimeSpan? currentExpiration = await _database.KeyTimeToLiveAsync(KeyLower(cacheKey), CommandFlags.PreferReplica);
            if (currentExpiration is null)
            {
                _logger.LogWarning("The key {cacheKey} could not be found.", cacheKey);
                return default;
            }

            TimeSpan newExpiration = (TimeSpan)(currentExpiration + TimeSpan.FromMinutes(cacheDurationInMinutes));

            bool succeeded = await _database.KeyExpireAsync(KeyLower(cacheKey), newExpiration, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not extend {cacheKey} key {@minute} minutes.", cacheKey, cacheDurationInMinutes);
            }

            return succeeded;
        });
    }

    public async Task<bool> RemoveAsync(string cacheKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            if (await _database.KeyExistsAsync(KeyLower(cacheKey), CommandFlags.PreferReplica) && !await _database.KeyDeleteAsync(KeyLower(cacheKey), CommandFlags.DemandMaster))
            {
                _logger.LogWarning("Failed to delete key {cacheKey} in Redis.", cacheKey);
                return default;
            }

            return true;
        });
    }

    public async Task<bool> RemoveKeysBySearchKeyAsync(string searchKey, SearchKeyType searchKeyType)
    {
        ArgumentException.ThrowIfNullOrEmpty(searchKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            searchKey = searchKeyType switch
            {
                SearchKeyType.EndsWith => $"*{searchKey}",
                SearchKeyType.StartsWith => $"{searchKey}*",
                SearchKeyType.Include => $"*{searchKey}*",
                _ => $"*{searchKey}*",
            };

            var redisKeys = _server.Keys(_redisServerSetting.DefaultDatabase, searchKey, flags: CommandFlags.PreferReplica).ToArray();
            if (redisKeys.Length == 0)
            {
                _logger.LogInformation("Key(s) for {@searchKey} searched in Redis could not be found.", searchKey);
                return default;
            }

            long totalNumberOfDeletedKeys = await _database.KeyDeleteAsync(redisKeys, CommandFlags.DemandMaster);
            if (0 >= totalNumberOfDeletedKeys)
            {
                _logger.LogError("A total of {@redisKeysCount} keys for {@searchKey} searched in Redis were found, but none of the keys could be deleted.", searchKey, redisKeys.Length);
                return default;
            }
            else if (totalNumberOfDeletedKeys != redisKeys.Length)
            {
                _logger.LogWarning("A total of {@redisKeysCount} keys for the searched keyword {@searchKey} were found in redis, but the total {@totalNumberOfDeletedKeys} keys were deleted.", searchKey, redisKeys.Length, totalNumberOfDeletedKeys);
                return true;
            }
            else
            {
                _logger.LogInformation("All keys belonging to {@searchKey} searched in redis have been deleted.", searchKey);
            }

            return true;
        });
    }

    public async Task ClearDatabaseAsync()
    {
        await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            await _server.FlushDatabaseAsync(_redisServerSetting.DefaultDatabase, CommandFlags.DemandMaster);
        });
    }

    public async Task ClearDatabaseAsync(int databaseNumber)
    {
        await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            await _server.FlushDatabaseAsync(databaseNumber, CommandFlags.DemandMaster);
        });
    }

    private static void CheckArguments<TValue>(string cacheKey, string hashField, TValue value)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(hashField);
        ArgumentNullException.ThrowIfNull(value);
    }

    private static void CheckArguments<TValue>(string cacheKey, TValue value)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentNullException.ThrowIfNull(value);
    }

    private string KeyLower(string cacheKey) => cacheKey.ToLower(_englishCultureInfo);
}
