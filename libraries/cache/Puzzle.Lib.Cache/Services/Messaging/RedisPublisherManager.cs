namespace Puzzle.Lib.Cache.Services.Messaging
{
    public sealed class RedisPublisherManager : IRedisPublisherService
    {
        private readonly ISubscriber _subscriber;

        public RedisPublisherManager(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public async Task<long> PublishAsync<TEvent>(string channelName, TEvent @event) where TEvent : RedisIntegrationBaseEvent
        {
            ArgumentException.ThrowIfNullOrEmpty(channelName);

            return await RedisRetryPolicies.AsyncRetryPolicy.ExecuteAsync(async () =>
            {
                return await _subscriber.PublishAsync(channelName, JsonSerializer.Serialize(@event), CommandFlags.FireAndForget);
            });
        }
    }
}
