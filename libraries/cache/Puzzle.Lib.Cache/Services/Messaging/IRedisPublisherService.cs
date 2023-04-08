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
        /// The number of clients that received the message *on the destination server*,
        /// note that this doesn't mean much in a cluster as clients can get the message through other nodes.
        Task<long> PublishAsync<TEvent>(string channelName, TEvent @event) where TEvent : RedisIntegrationBaseEvent;
    }
}
