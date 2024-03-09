namespace Puzzle.Lib.Cache.Services.Cache;

/// <summary>
/// Defines methods for interacting with Redis cache.
/// </summary>
public interface IRedisService
{
    /// <summary>
    /// Checks if the specified cache key exists.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <returns>A task indicating whether the specified cache key exists.</returns>
    /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
    Task<bool> ExistsAsync(string cacheKey);

    /// <summary>
    /// Increments the value of the specified cacheKey by the specified increment value.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="increment">The value to increment by.</param>
    /// <returns>A task indicating the new value of the specified cacheKey.</returns>
    /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
    Task<long> IncrementAsync(string cacheKey, int increment);

    /// <summary>
    /// Decrements the value of the specified cache key by the specified decrement value.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="decrement">The value to decrement by.</param>
    /// <returns>A task indicating the new value of the specified cache key.</returns>
    /// <exception cref="ArgumentNullException">thrown when cache key is empty or null</exception>
    Task<long> DecrementAsync(string cacheKey, int decrement);

    /// <summary>
    /// Checks whether a field exists in a hash structure identified by the cache key.
    /// </summary>
    /// <param name="cacheKey">The key identifying the hash structure in the cache.</param>
    /// <param name="hashField">The field within the hash structure to check for existence.</param>
    /// <returns>True if the field exists; otherwise, false.</returns>
    Task<bool> HashExistsAsync(string cacheKey, string hashField);

    /// <summary>
    /// Retrieves all fields and values from the hash stored at the specified cache key.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <returns>A task containing the retrieved fields and values from the hash.</returns>
    Task<IEnumerable<KeyValuePair<string, string>>> HashGetAllAsync(string cacheKey);

    /// <summary>
    /// Retrieves the value associated with the specified hash field from the hash stored at the specified cache key and deserializes it to the specified type.
    /// </summary>
    /// <typeparam name="TData">The type to deserialize the value to.</typeparam>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="hashField">The field to retrieve the value for.</param>
    /// <returns>A task containing the deserialized value associated with the specified hash field.</returns>
    Task<TData> HashGetAsync<TData>(string cacheKey, string hashField);

    /// <summary>
    /// Sets the value of a hash field to the specified value in the hash stored at the specified cache key.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="hashField">The hash field to set the value for.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>A task containing a boolean value indicating whether the hash field was updated.</returns>
    Task<bool> HashSetAsync<TValue>(string cacheKey, string hashField, TValue value);

    /// <summary>
    /// Removes the specified hash field from the hash stored at the specified cache key.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="hashField">The hash field to remove.</param>
    /// <returns>A task containing a boolean value indicating whether the hash field was deleted.</returns>
    Task<bool> HashDeleteAsync(string cacheKey, string hashField);

    /// <summary>
    /// Increments the score of the specified member in the sorted set stored at the specified cache key.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="memberKey">The member whose score should be incremented.</param>
    /// <param name="increment">The amount by which to increment the score.</param>
    /// <returns>A task containing the new score of the member in the sorted set.</returns>
    Task<int> SortedSetIncrementAsync(string cacheKey, string memberKey, int increment);

    /// <summary>
    /// Retrieves a sorted list of elements in the cache with the specified key in descending order of their scores.
    /// </summary>
    /// <typeparam name="TData">The type of the elements in the list.</typeparam>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="start">The zero-based index of the start of the range to retrieve.</param>
    /// <param name="stop">The zero-based index of the end of the range to retrieve.</param>
    /// <returns>A list of elements in the cache with the specified key in descending order of their scores.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the cache key is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when start or stop parameter is negative.</exception>
    /// <exception cref="RedisException">Thrown when an error occurred while executing the command in Redis.</exception>
    /// <remarks>
    /// The default values for start and stop parameters retrieve the entire list. 
    /// </remarks>
    Task<IEnumerable<TData>> GetSortedListInDescendingOrderOfScoreAsync<TData>(string cacheKey, int start = 0, int stop = -1) where TData : struct;

    /// <summary>
    /// Retrieves a sorted list of elements in the cache with the specified key in ascending order of their scores.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="start">The zero-based index of the start of the range to retrieve.</param>
    /// <param name="stop">The zero-based index of the end of the range to retrieve.</param>
    /// <returns>A list of elements in the cache with the specified key in ascending order of their scores.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the cache key is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when start or stop parameter is negative.</exception>
    /// <exception cref="RedisException">Thrown when an error occurred while executing the command in Redis.</exception>
    /// <remarks>
    /// The default values for start and stop parameters retrieve the entire list. 
    /// </remarks>
    Task<IEnumerable<TData>> GetSortedListInAscendingOrderOfScoreAsync<TData>(string cacheKey, int start = 0, int stop = -1) where TData : struct;

    /// <summary>
    /// Removes the specified member from the sorted set with the given cache key.
    /// </summary>
    /// <param name="cacheKey">The key of the sorted set in the cache.</param>
    /// <param name="memberKey">The key of the member to remove from the sorted set.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a value indicating whether the member was successfully removed from the sorted set.</returns>
    Task<bool> SortedSetDeleteAsync(string cacheKey, string memberKey);

    /// <summary>
    /// Checks whether a set identified by the cache key contains a specific value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to check for existence.</typeparam>
    /// <param name="cacheKey">The key identifying the set in the cache.</param>
    /// <param name="value">The value to check for existence in the set.</param>
    /// <returns>True if the value exists in the set; otherwise, false.</returns>
    Task<bool> SetExistsAsync<TValue>(string cacheKey, TValue value);

    /// <summary>
    /// Retrieves all members of a set identified by the cache key.
    /// </summary>
    /// <typeparam name="TData">The type of data stored in the set.</typeparam>
    /// <param name="cacheKey">The key identifying the set in the cache.</param>
    /// <returns>An asynchronous operation that yields a collection of set members.</returns>
    Task<IEnumerable<TData>> SetMembersAsync<TData>(string cacheKey);

    /// <summary>
    /// Adds a value to a set identified by the cache key.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to add.</typeparam>
    /// <param name="cacheKey">The key identifying the set in the cache.</param>
    /// <param name="value">The value to add to the set.</param>
    /// <returns>True if the value was added to the set; otherwise, false.</returns>
    Task<bool> SetAddAsync<TValue>(string cacheKey, TValue value);

    /// <summary>
    /// Removes a value from a set identified by the cache key.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to remove.</typeparam>
    /// <param name="cacheKey">The key identifying the set in the cache.</param>
    /// <param name="value">The value to remove from the set.</param>
    /// <returns>True if the value was removed from the set; otherwise, false.</returns>
    Task<bool> SetRemoveAsync<TValue>(string cacheKey, TValue value);

    /// <summary>
    /// Gets the value of the specified key from cache as deserialized object of type TResponse.
    /// </summary>
    /// <typeparam name="TResponse">Type of the deserialized object</typeparam>
    /// <param name="cacheKey">Key of the cached item</param>
    /// <returns>Deserialized object of type TResponse</returns>
    /// <exception cref="ArgumentNullException">Thrown when cacheKey is null or empty.</exception>
    Task<TResponse> StringGetAsync<TResponse>(string cacheKey) where TResponse : class, new();

    /// <summary>
    /// Set the specified key-value pair in the cache asynchronously. If the key already exists, it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of value to be stored in the cache.</typeparam>
    /// <param name="cacheKey">The key to store the value under in the cache.</param>
    /// <param name="value">The value to store in the cache.</param>
    /// <returns>A boolean indicating whether the operation succeeded.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided cache key is null or empty.</exception>
    Task<bool> StringSetAsync<TValue>(string cacheKey, TValue value);

    /// <summary>
    /// Sets a cache entry for the specified key and value, with the specified cache duration.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to be cached.</typeparam>
    /// <param name="cacheKey">The key of the cache entry.</param>
    /// <param name="value">The value to be cached.</param>
    /// <param name="cacheDurationInMinutes">The duration of the cache entry.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a boolean value indicating whether the operation succeeded.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="cacheKey"/> or <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="cacheKey"/> is an empty string.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the cache service is not available.</exception>
    Task<bool> StringSetAsync<TValue>(string cacheKey, TValue value, int cacheDurationInMinutes);

    /// <summary>
    /// Updates an existing value in the cache with the provided key. If the key does not exist, this method returns false.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to store in the cache.</typeparam>
    /// <param name="cacheKey">The key to use when storing the value in the cache.</param>
    /// <param name="value">The value to store in the cache.</param>
    /// <returns>True if the value was successfully updated; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="cacheKey"/> is null.</exception>
    /// <exception cref="RedisConnectionException">Thrown when a connection to the Redis server cannot be established.</exception>
    /// <exception cref="RedisException">Thrown when there is an error communicating with the Redis server.</exception>
    /// <remarks>The existing TTL (time to live) of the cache key is preserved.</remarks>
    ///</summary>
    Task<bool> ChangeStringAsync<TValue>(string cacheKey, TValue value);

    /// <summary>
    /// Extends the expiration time of a cache key by the specified extend time.
    /// </summary>
    /// <param name="cacheKey">The cache key to extend the expiration time for.</param>
    /// <param name="cacheDurationInMinutes">The amount of time to extend the expiration time by.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning a <see cref="bool"/> indicating whether the operation was successful.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="cacheKey"/> parameter is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="cacheDurationInMinutes"/> parameter is negative.</exception>
    Task<bool> ExtendCacheDurationAsync(string cacheKey, int cacheDurationInMinutes);

    /// <summary>
    /// Removes the cache entry with the specified key.
    /// </summary>
    /// <param name="cacheKey">The key of the cache entry to remove.</param>
    /// <returns>True if the cache entry was successfully removed, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when cacheKey is null or empty.</exception>
    Task<bool> RemoveAsync(string cacheKey);

    /// <summary>
    /// Removes all cache keys that match the specified search key pattern.
    /// </summary>
    /// <param name="searchKey">The search key pattern.</param>
    /// <param name="searchKeyType">The type of the search key pattern.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the removal operation is successful or not.</returns>
    /// <exception cref="ArgumentNullException">thrown when search key is empty or null</exception>
    Task<bool> RemoveKeysBySearchKeyAsync(string searchKey, SearchKeyType searchKeyType);

    /// <summary>
    /// Clears the entire database by removing all keys.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a boolean indicating if the operation was successful.</returns>
    Task ClearDatabaseAsync();

    /// <summary>
    /// Removes all the keys of the currently selected database or a specific database.
    /// </summary>
    /// <param name="databaseNumber">The number of the database to be cleared. If not provided, the currently selected database will be cleared.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a value indicating whether the operation was successful.</returns>
    /// <exception cref="RedisException">Thrown when an error occurred while executing the Redis command.</exception>
    Task ClearDatabaseAsync(int databaseNumber);
}
