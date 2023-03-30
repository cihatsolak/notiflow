namespace Puzzle.Lib.Cache.Services.Messaging
{
    /// <summary>
    /// Defines a contract for a service that publishes events to a Redis channel.
    /// </summary>
    public interface IRedisPublisherService
    {
        /// <summary>
        /// Publishes the specified event to the specified Redis channel.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to publish. Must inherit from RedisIntegrationBaseEvent.</typeparam>
        /// <param name="channelName">The name of the Redis channel to publish the event to.</param>
        /// <param name="event">The event to publish.</param>
        /// <returns>A task that represents the asynchronous publish operation.</returns>
        Task PublishAsync<TEvent>(string channelName, TEvent @event) where TEvent : RedisIntegrationBaseEvent;
    }
}
