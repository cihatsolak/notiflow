namespace Puzzle.Lib.Cache.Tests.Services
{
    public class RedisPublisherManagerTests
    {
        [Fact]
        public async Task PublishAsync_WithCorrectChannelAndEvent_ShouldCallSubscriber()
        {
            // Arrange
            var channelName = "test-channel";
            var testEvent = new TestRedisIntegrationEvent();
            var testEventJson = JsonSerializer.Serialize(testEvent);
            var subscriberMock = new Mock<ISubscriber>();
            var redisPublisherManager = new RedisPublisherManager(subscriberMock.Object);

            // Act
            await redisPublisherManager.PublishAsync(channelName, testEvent);

            // Assert
            subscriberMock.Verify(
                x => x.PublishAsync(channelName, testEventJson, CommandFlags.FireAndForget),
                Times.Once);
        }

        [Fact]
        public async Task PublishAsync_WhenChannelNameIsNullOrEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            var channelName = string.Empty;
            var testEvent = new TestRedisIntegrationEvent();
            var subscriberMock = new Mock<ISubscriber>();
            var redisPublisherManager = new RedisPublisherManager(subscriberMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => redisPublisherManager.PublishAsync(channelName, testEvent));
        }
    }
}
