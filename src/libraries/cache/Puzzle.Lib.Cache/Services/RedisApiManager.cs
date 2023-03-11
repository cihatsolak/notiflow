namespace Puzzle.Lib.Cache.Services
{
    internal sealed class RedisApiManager : IRedisService
    {
        private readonly IDatabase _database;
        private readonly IServer _server;
        private readonly int _defaultDatabase;

        public RedisApiManager(
             IDatabase database,
             IServer server,
             int defaultDatabase)
        {
            _database = database;
            _server = server;
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
            RedisArgumentException.ThrowIfNegativeNumber(increment, nameof(increment));

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                long result = await _database.StringIncrementAsync(cacheKey, increment, CommandFlags.DemandMaster);
                if (0 >= result)
                {
                    Log.Warning("The key value {@cacheKey} could not be incremented by {@increment}.", cacheKey, increment);
                    return default;
                }

                return result;
            });
        }

        public async Task<long> DecrementAsync(string cacheKey, int decrement)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);
            RedisArgumentException.ThrowIfNegativeNumber(decrement, nameof(decrement));

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                long result = await _database.StringDecrementAsync(cacheKey, decrement, CommandFlags.DemandMaster);
                if (0 >= result)
                {
                    Log.Warning("The {@cacheKey} key value has been reduced by {@increment}.", cacheKey, decrement);
                    return default;
                }

                return result;
            });
        }

        public async Task<HashEntry[]> HashGetAllAsync(string cacheKey)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                var hashEntries = await _database.HashGetAllAsync(cacheKey, CommandFlags.PreferReplica);
                if (!hashEntries.Any())
                {
                    Log.Warning("Data for key {@cacheKey} not found.", cacheKey);
                    return default;
                }

                return hashEntries;
            });
        }

        public async Task<TResponse> HashGetAsync<TResponse>(string cacheKey, string hashField)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                var hashEntry = await _database.HashGetAsync(cacheKey, hashField, CommandFlags.PreferReplica);
                if (!hashEntry.HasValue)
                {
                    Log.Warning("Data for key {@cacheKey} not found.", cacheKey);
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
                bool succeeded = await _database.HashSetAsync(cacheKey, hashField, value, flags: CommandFlags.DemandMaster);
                if (!succeeded)
                {
                    Log.Warning("The data for the {@cacheKey} key value could not be transferred to the redis.", cacheKey);
                    return default;
                }

                return true;
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
                    Log.Warning("Unable to delete data for key value {@cacheKey}.", cacheKey);
                    return default;
                }

                return true;
            });
        }

        public async Task<int> SortedSetIncrementAsync(string cacheKey, string memberKey, int increment)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);
            ArgumentException.ThrowIfNullOrEmpty(memberKey);
            RedisArgumentException.ThrowIfNegativeNumber(increment, nameof(increment));

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                double result = await _database.SortedSetIncrementAsync(cacheKey, memberKey, increment, CommandFlags.DemandMaster);
                if (result == 0)
                {
                    Log.Warning("Could not update member {@memberKey} in the ordered list of key value {@cacheKey}.", cacheKey, memberKey);
                    return default;
                }

                return Convert.ToInt32(result);
            });
        }

        public async Task<List<T>> GetSortedListByScoreAsync<T>(string cacheKey, int start = 0, int stop = -1, Order order = Order.Descending) where T : struct
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                var redisValues = await _database.SortedSetRangeByRankAsync(cacheKey, start, stop, order, CommandFlags.PreferReplica);
                if (!redisValues.Any())
                {
                    Log.Warning("{@cacheKey} anahtar değerine ait sıralı listede veri bulunamadı. | start: {@start}, stop: {@stop}", cacheKey, start, stop);
                    return default;
                }

                return redisValues.Select(redisValue => (T)Convert.ChangeType(redisValue, typeof(T))).ToList();
            });
        }

        public async Task<TResponse> GetAsync<TResponse>(string cacheKey) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                var redisValue = await _database.StringGetAsync(cacheKey);
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
                bool succeeded = await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(value), flags: CommandFlags.DemandMaster);
                if (!succeeded)
                {
                    Log.Warning("Could not transfer data {@cacheKey} to redis.", cacheKey);
                    return default;
                }

                return true;
            });
        }

        public async Task<bool> SetWithExpiryDateAsync<TValue>(string cacheKey, TValue value, AbsoluteExpiration absoluteExpiration)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);
            ArgumentNullException.ThrowIfNull(value);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                bool succeeded = await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(value), flags: CommandFlags.DemandMaster);
                if (!succeeded)
                {
                    Log.Warning("Could not transfer data {@cacheKey} to redis.", cacheKey);
                    return default;
                }

                if (!await _database.KeyExpireAsync(cacheKey, DateTime.Now.AddMinutes(Convert.ToInt32(absoluteExpiration))))
                {
                    Log.Warning("Unable to add the expiration time of the cache {@cacheKey}.", cacheKey);
                    return default;
                }

                return true;
            });
        }

        public async Task<bool> SetWithExtendTimeAsync<TValue>(string cacheKey, TValue value, ExtendTime extendTime)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);
            ArgumentNullException.ThrowIfNull(value);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                if (!await _database.KeyExistsAsync(cacheKey))
                {
                    Log.Warning("The key {@cacheKey} could not be found.", cacheKey);
                    return default;
                }

                var expireTime = await _database.KeyExpireTimeAsync(cacheKey, CommandFlags.PreferReplica);
                if (!expireTime.HasValue)
                {
                    Log.Warning("The expiration time for the key {@cacheKey} could not be found.", cacheKey);
                    return default;
                }

                bool succeeded = await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(value), flags: CommandFlags.DemandMaster);
                if (!succeeded)
                {
                    Log.Warning("Could not transfer data {@cacheKey} to redis.", cacheKey);
                    return default;
                }

                bool succedeed = await _database.KeyExpireAsync(cacheKey, expireTime.Value.AddMinutes(Convert.ToInt32(extendTime)), CommandFlags.DemandMaster);
                if (!succedeed)
                {
                    Log.Warning("Could not extend {@cacheKey} key {@minute} minutes.", cacheKey, Convert.ToInt32(extendTime));
                    return default;
                }

                return true;
            });
        }

        public async Task<bool> ExtendCacheKeyTimeAsync(string cacheKey, ExtendTime extendTime)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                if (!await _database.KeyExistsAsync(cacheKey))
                {
                    Log.Warning("The key {@cacheKey} could not be found.", cacheKey);
                    return default;
                }

                var expireTime = await _database.KeyExpireTimeAsync(cacheKey, CommandFlags.PreferReplica);
                if (!expireTime.HasValue)
                {
                    Log.Warning("The key {@cacheKey} could not be found.", cacheKey);
                    return default;
                }

                var succedeed = await _database.KeyExpireAsync(cacheKey, expireTime.Value.AddMinutes(Convert.ToInt32(extendTime)), CommandFlags.DemandMaster);
                if (!succedeed)
                {
                    Log.Warning("Could not extend {@cacheKey} key {@minute} minutes.", cacheKey, Convert.ToInt32(extendTime));
                    return default;
                }

                return true;
            });
        }

        public async Task<bool> RemoveAsync(string cacheKey)
        {
            ArgumentException.ThrowIfNullOrEmpty(cacheKey);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                if (await _database.KeyExistsAsync(cacheKey, CommandFlags.PreferReplica) && !await _database.KeyDeleteAsync(cacheKey, CommandFlags.PreferReplica))
                {
                    Log.Warning("Failed to delete key {@cacheKey} in Redis.", cacheKey);
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
                    Log.Information("Key(s) for {@searchKey} searched in Redis could not be found.", searchKey);
                    return default;
                }

                var totalNumberOfDeletedKeys = await _database.KeyDeleteAsync(redisKeys, CommandFlags.DemandMaster);
                if (0 >= totalNumberOfDeletedKeys)
                {
                    Log.Error("A total of {@redisKeysCount} keys for {@searchKey} searched in Redis were found, but none of the keys could be deleted.", searchKey, redisKeys.Length);
                    return default;
                }
                else if (totalNumberOfDeletedKeys > 0 && totalNumberOfDeletedKeys != redisKeys.Length)
                {
                    Log.Warning("A total of {@redisKeysCount} keys for the searched keyword {@searchKey} were found in redis, but the total {@totalNumberOfDeletedKeys} keys were deleted.", searchKey, redisKeys.Length, totalNumberOfDeletedKeys);
                    return true;
                }
                else
                {
                    Log.Information("All keys belonging to {@searchKey} searched in redis have been deleted.", searchKey);
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
}
