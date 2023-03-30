namespace Puzzle.Lib.Cache.Tests.Services
{
    public class RedisPublisherManagerTests
    {
        [Fact]
        public async Task PublishAsync_ShouldCallSubscriber_WithCorrectChannelAndEvent()
        {
            // Arrange
            var channelName = "test-channel";
            var testEvent = new TestEvent();
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
        public async Task PublishAsync_ShouldThrowArgumentException_WhenChannelNameIsNullOrEmpty()
        {
            // Arrange
            var channelName = string.Empty;
            var testEvent = new TestEvent();
            var subscriberMock = new Mock<ISubscriber>();
            var redisPublisherManager = new RedisPublisherManager(subscriberMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => redisPublisherManager.PublishAsync(channelName, testEvent));
        }
    }

    public class TestEvent : RedisIntegrationBaseEvent
    {
        // Define test event properties here
    }
}
