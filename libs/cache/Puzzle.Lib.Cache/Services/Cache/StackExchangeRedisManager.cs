namespace Puzzle.Lib.Cache.Services.Cache;

internal sealed class StackExchangeRedisManager : IRedisService
{
    private readonly IDatabase _database;
    private readonly IServer _server;
    private readonly ILogger<StackExchangeRedisManager> _logger;
    private readonly int _defaultDatabase;

    public StackExchangeRedisManager(
         IDatabase database,
         IServer server,
         ILogger<StackExchangeRedisManager> logger,
         int defaultDatabase)
    {
        _database = database;
        _server = server;
        _logger = logger;
        _defaultDatabase = defaultDatabase;
    }

    public async Task<bool> ExistsAsync(string cacheKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await _database.KeyExistsAsync(cacheKey, CommandFlags.PreferReplica);
        });
    }

    public async Task<long> IncrementAsync(string cacheKey, int increment)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        CacheArgumentException.ThrowIfNegativeNumber(increment);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            long result = await _database.StringIncrementAsync(cacheKey, increment, CommandFlags.DemandMaster);
            if (0 >= result)
            {
                _logger.LogWarning("The key value {@cacheKey} could not be incremented by {@increment}.", cacheKey, increment);
            }

            return result;
        });
    }

    public async Task<long> DecrementAsync(string cacheKey, int decrement)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        CacheArgumentException.ThrowIfNegativeNumber(decrement);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            long result = await _database.StringDecrementAsync(cacheKey, decrement, CommandFlags.DemandMaster);
            if (0 >= result)
            {
                _logger.LogWarning("The {@cacheKey} key value has been reduced by {@increment}.", cacheKey, decrement);
            }

            return result;
        });
    }

    public async Task<IEnumerable<KeyValuePair<string, string>>> HashGetAllAsync(string cacheKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var hashEntries = await _database.HashGetAllAsync(cacheKey, CommandFlags.PreferReplica);
            if (!hashEntries.Any())
            {
                _logger.LogWarning("Data for key {@cacheKey} not found.", cacheKey);
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            return hashEntries.Select(hashEntry => new KeyValuePair<string, string>(hashEntry.Name, hashEntry.Value));
        });
    }

    public async Task<TResponse> HashGetAsync<TResponse>(string cacheKey, string hashField)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(hashField);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var hashEntry = await _database.HashGetAsync(cacheKey, hashField, CommandFlags.PreferReplica);
            if (!hashEntry.HasValue)
            {
                _logger.LogWarning("Data for key {@cacheKey} not found.", cacheKey);
                return default;
            }

            return (TResponse)Convert.ChangeType(hashEntry, typeof(TResponse));
        });
    }

    public async Task<bool> HashSetAsync(string cacheKey, string hashField, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(hashField);
        ArgumentException.ThrowIfNullOrEmpty(value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.HashSetAsync(cacheKey, hashField, value, When.Always, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("The data for the {@cacheKey} key value could not be transferred to the redis.", cacheKey);
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
            bool succeeded = await _database.HashDeleteAsync(cacheKey, hashField, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Unable to delete data for key value {@cacheKey}.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<int> SortedSetIncrementAsync(string cacheKey, string memberKey, int increment)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentException.ThrowIfNullOrEmpty(memberKey);
        CacheArgumentException.ThrowIfNegativeNumber(increment);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            double result = await _database.SortedSetIncrementAsync(cacheKey, memberKey, increment, CommandFlags.DemandMaster);
            if (result == 0)
            {
                _logger.LogWarning("Could not update member {@memberKey} in the ordered list of key value {@cacheKey}.", cacheKey, memberKey);
            }

            return (int)result;
        });
    }

    public async Task<IEnumerable<TData>> GetSortedListInDescendingOrderOfScoreAsync<TData>(string cacheKey, int start = 0, int stop = -1) where TData : struct
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        CacheArgumentException.ThrowIfNegativeNumber(start);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var redisValues = await _database.SortedSetRangeByRankAsync(cacheKey, start, stop, Order.Descending, CommandFlags.PreferReplica);
            if (!redisValues.Any())
            {
                _logger.LogWarning("No data found in the ordered list of key value {@cacheKey}. | start: {@start}, stop: {@stop}", cacheKey, start, stop);
                return Enumerable.Empty<TData>();
            }

            return redisValues.Select(redisValue => (TData)Convert.ChangeType(redisValue, typeof(TData)));
        });
    }

    public async Task<IEnumerable<TData>> GetSortedListInAscendingOrderOfScoreAsync<TData>(string cacheKey, int start = 0, int stop = -1) where TData : struct
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        CacheArgumentException.ThrowIfNegativeNumber(start);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var redisValues = await _database.SortedSetRangeByRankAsync(cacheKey, start, stop, Order.Ascending, CommandFlags.PreferReplica);
            if (!redisValues.Any())
            {
                _logger.LogWarning("No data found in the ordered list of key value {@cacheKey}. | start: {@start}, stop: {@stop}", cacheKey, start, stop);
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
            bool succeeded = await _database.SortedSetRemoveAsync(cacheKey, memberKey, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not delete member {@memberKey} in the ordered list of key value {@cacheKey}.", cacheKey, memberKey);
            }

            return succeeded;
        });
    }

    public async Task<TResponse> GetAsync<TResponse>(string cacheKey) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            var redisValue = await _database.StringGetAsync(cacheKey, CommandFlags.PreferReplica);
            if (!redisValue.HasValue)
                return default;

            return JsonSerializer.Deserialize<TResponse>(redisValue);
        });
    }

    public async Task<bool> SetAsync<TValue>(string cacheKey, TValue value)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentNullException.ThrowIfNull(value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(value), null, When.Always, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not transfer data {@cacheKey} to redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> SetAsync<TValue>(string cacheKey, TValue value, CacheDuration cacheDuration)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentNullException.ThrowIfNull(value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(value), TimeSpan.FromMinutes((int)cacheDuration), When.Always, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not transfer data {@cacheKey} to redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> ChangeAsync<TValue>(string cacheKey, TValue value)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);
        ArgumentNullException.ThrowIfNull(value);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            bool succeeded = await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(value), null, true, When.Exists, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not transfer data {@cacheKey} to redis.", cacheKey);
            }

            return succeeded;
        });
    }

    public async Task<bool> ExtendCacheDurationAsync(string cacheKey, CacheDuration cacheDuration)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            TimeSpan? currentExpiration = await _database.KeyTimeToLiveAsync(cacheKey, CommandFlags.PreferReplica);
            if (currentExpiration is null)
            {
                _logger.LogWarning("The key {@cacheKey} could not be found.", cacheKey);
                return default;
            }

            TimeSpan newExpiration = (TimeSpan)(currentExpiration + TimeSpan.FromMinutes((int)cacheDuration));

            bool succeeded = await _database.KeyExpireAsync(cacheKey, newExpiration, CommandFlags.DemandMaster);
            if (!succeeded)
            {
                _logger.LogWarning("Could not extend {@cacheKey} key {@minute} minutes.", cacheKey, (int)cacheDuration);
            }

            return succeeded;
        });
    }

    public async Task<bool> RemoveAsync(string cacheKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(cacheKey);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            if (await _database.KeyExistsAsync(cacheKey, CommandFlags.PreferReplica) && !await _database.KeyDeleteAsync(cacheKey, CommandFlags.DemandMaster))
            {
                _logger.LogWarning("Failed to delete key {@cacheKey} in Redis.", cacheKey);
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

            var redisKeys = _server.Keys(_defaultDatabase, searchKey, flags: CommandFlags.PreferReplica).ToArray();
            if (redisKeys is null || !redisKeys.Any())
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
            else if (totalNumberOfDeletedKeys > 0 && totalNumberOfDeletedKeys != redisKeys.Length)
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
            await _server.FlushDatabaseAsync(_defaultDatabase, CommandFlags.DemandMaster);
        });
    }

    public async Task ClearDatabaseAsync(int databaseNumber)
    {
        await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            await _server.FlushDatabaseAsync(databaseNumber, CommandFlags.DemandMaster);
        });
    }
}
