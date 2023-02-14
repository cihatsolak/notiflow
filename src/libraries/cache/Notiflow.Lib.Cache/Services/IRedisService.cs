namespace Notiflow.Lib.Cache.Services
{
    public interface IRedisService
    {
        /// <summary>
        /// Is there data from the cache
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<bool> ExistsAsync(string cacheKey);

        /// <summary>
        /// Increases key value by increment amount
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="increment">increment value</param>
        /// <returns>last value of data</returns>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<int> IncrementAsync(string cacheKey, int increment);

        /// <summary>
        /// Decreases key value by decrease amount
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="decrement">decrement value</param>
        /// <returns>last value of data</returns>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<int> DecrementAsync(string cacheKey, int decrement);

        /// <summary>
        /// Lists the table's data by key value
        /// </summary>
        /// <param name="cacheKey">cache key of the table</param>
        /// <returns>type of hash entry array</returns>
        Task<HashEntry[]> HashGetAllAsync(string cacheKey);

        /// <summary>
        /// Hash get
        /// </summary>
        /// <param name="cacheKey">key group</param>
        /// <param name="hashField">key</param>
        /// <returns>type of hash value</returns>
        Task<TResponse> HashGetAsync<TResponse>(string cacheKey, string hashField);

        /// <summary>
        /// Hash set
        /// </summary>
        /// <param name="cacheKey">key group</param>
        /// <param name="hashField">key</param>
        /// <param name="value">value</param>
        Task<bool> HashSetAsync(string cacheKey, string hashField, string value);

        /// <summary>
        /// Hash delete
        /// </summary>
        /// <param name="cacheKey">key group</param>
        /// <param name="hashField">key</param>
        Task<bool> HashDeleteAsync(string cacheKey, string hashField);

        /// <summary>
        /// Sorted set increment
        /// </summary>
        /// <param name="cacheKey">cache keys</param>
        /// <param name="memberKey">member key</param>
        /// <param name="increment">increment value</param>
        Task<int> SortedSetIncrementAsync(string cacheKey, string memberKey, int increment);

        /// <summary>
        /// Get sorted list by score
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="cacheKey">cache keys</param>
        /// <param name="start">start score</param>
        /// <param name="stop">stop score</param>
        /// <param name="order">default: descending</param>
        /// <returns>type of T list</returns>
        Task<List<T>> GetSortedListByScoreAsync<T>(string cacheKey, int start = 0, int stop = -1, Order order = Order.Descending) where T : struct;

        /// <summary>
        /// Lists data by cache key
        /// </summary>
        /// <typeparam name="TValue">safe data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <returns>designated safe type</returns>
        Task<TValue> GetAsync<TValue>(string cacheKey);

        /// <summary>
        /// Add data to cache
        /// </summary>
        /// <typeparam name="TValue">safe data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">value to be cached</param>
        /// <returns>success status</returns>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<bool> SetAsync<TValue>(string cacheKey, TValue value);

        /// <summary>
        /// Add data to cache with specified expiry time
        /// </summary>
        /// <typeparam name="TValue">safe data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">value to be cached</param>
        /// <param name="absoluteExpiration">absolute expiration</param>
        /// <returns>success status</returns>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<bool> SetWithExpiryDateAsync<TValue>(string cacheKey, TValue value, AbsoluteExpiration absoluteExpiration);

        /// <summary>
        /// Update cache data and extend its lifetime by specified time
        /// </summary>
        /// <typeparam name="TValue">safe data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">value to be cached</param>
        /// <param name="extendTime">time in minutes</param>
        /// <returns>success status</returns>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<bool> SetWithExtendTimeAsync<TValue>(string cacheKey, TValue value, ExtendTime extendTime);

        /// <summary>
        /// The lifetime of the cache data is extended for the specified time
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="extendTime">time in minutes</param>
        /// <returns>success status</returns>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<bool> ExtendCacheKeyTimeAsync(string cacheKey, ExtendTime extendTime);

        /// <summary>
        /// Deletes data based on cache key
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <returns>success status</returns>
        /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
        Task<bool> RemoveAsync(string cacheKey);

        /// <summary>
        /// Data is deleted by search key and search type
        /// </summary>
        /// <param name="searchKey">search key</param>
        /// <param name="searchKeyType">search key type</param>
        /// <returns>success status</returns>
        /// <exception cref="ArgumentNullException">thrown when search key is empty or null</exception>
        Task<bool> RemoveKeysBySearchKeyAsync(string searchKey, SearchKeyType searchKeyType);

        /// <summary>
        /// Deletes data from the default database
        /// </summary>
        ValueTask ClearDatabaseAsync();

        /// <summary>
        /// Deletes the data of the specified database
        /// </summary>
        /// <param name="databaseNumber">database number whose data is to be deleted</param>
        ValueTask ClearDatabaseAsync(int databaseNumber);
    }
}
