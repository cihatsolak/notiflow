namespace Puzzle.Lib.Cache.Services.Messaging;

public sealed class RedisPublisherManager(ISubscriber subscriber) : IRedisPublisherService
{
    public async Task<long> PublishAsync<TEvent>(string channelName, TEvent @event) where TEvent : RedisIntegrationBaseEvent
    {
        CheckArguments(channelName, @event);

        RedisChannel redisChannel = new(channelName, RedisChannel.PatternMode.Literal);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await subscriber.PublishAsync(redisChannel, JsonSerializer.Serialize(@event), CommandFlags.FireAndForget);
        });
    }

    private static void CheckArguments<TEvent>(string channelName, TEvent @event)
    {
        ArgumentException.ThrowIfNullOrEmpty(channelName);
        ArgumentNullException.ThrowIfNull(@event);
    }
}