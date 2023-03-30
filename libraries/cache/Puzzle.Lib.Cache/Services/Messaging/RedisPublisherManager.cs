namespace Puzzle.Lib.Cache.Services.Messaging
{
    public sealed class RedisPublisherManager : IRedisPublisherService
    {
        private readonly ISubscriber _subscriber;

        public RedisPublisherManager(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public async Task PublishAsync<TEvent>(string channelName, TEvent @event) where TEvent : RedisIntegrationBaseEvent
        {
            ArgumentException.ThrowIfNullOrEmpty(channelName);

            await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                await _subscriber.PublishAsync(channelName, JsonSerializer.Serialize(@event), CommandFlags.FireAndForget);
            });
        }
    }
}
