namespace Puzzle.Lib.Cache.Services.Messaging;

public sealed class RedisPublisherManager : IRedisPublisherService
{
    private readonly ISubscriber _subscriber;

    public RedisPublisherManager(ISubscriber subscriber)
    {
        _subscriber = subscriber;
    }

    public async Task<long> PublishAsync<TEvent>(string channelName, TEvent @event) where TEvent : RedisIntegrationBaseEvent
    {
        CheckParameters(channelName, @event);

        return await PublishToRedisAsync(channelName, @event);
    }

    private static void CheckParameters<TEvent>(string channelName, TEvent @event)
    {
        ArgumentException.ThrowIfNullOrEmpty(channelName);
        ArgumentNullException.ThrowIfNull(@event);
    }

    private async Task<long> PublishToRedisAsync<TEvent>(string channelName, TEvent @event)
    {
        RedisChannel redisChannel = new(channelName, RedisChannel.PatternMode.Literal);

        return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await _subscriber.PublishAsync(redisChannel, JsonSerializer.Serialize(@event), CommandFlags.FireAndForget);
        });
    }
}
