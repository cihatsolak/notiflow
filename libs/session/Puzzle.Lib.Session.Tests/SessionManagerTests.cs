using Microsoft.AspNetCore.Http;
using Moq;
using Puzzle.Lib.Session.Services;
using System.Text;
using System.Text.Json;

namespace Puzzle.Lib.Session.Tests
{
    public class TestData
    {
        public string Property1 { get; set; }
        public int Property2 { get; set; }
    }

    public class SessionManagerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public SessionManagerTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        [Fact]
        public void Get_WhenKeyExists_ReturnsDeserializedData()
        {
            // Arrange
            var data = new TestData { Property1 = "value1", Property2 = 2 };
            var serializedData = JsonSerializer.Serialize(data);
            var sessionData = Encoding.UTF8.GetBytes(serializedData);

            _httpContextAccessorMock.Setup(x => x.HttpContext.Session.TryGetValue("test-key", out sessionData))
                                    .Returns(true);

            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act
            var result = sessionManager.Get<TestData>("test-key");

            // Assert
            Assert.Equal(data.Property1, result.Property1);
            Assert.Equal(data.Property2, result.Property2);
        }

        [Fact]
        public void Get_WhenKeyDoesNotExist_ReturnsDefault()
        {
            // Arrange
            _httpContextAccessorMock.Setup(x => x.HttpContext.Session.TryGetValue("test-key", out It.Ref<byte[]>.IsAny))
                                    .Returns(false);

            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act
            var result = sessionManager.Get<TestData>("test-key");

            // Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public void Set_WhenDataIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => sessionManager.Set<TestData>("test-key", null));
        }

        [Fact]
        public void Set_WhenKeyIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => sessionManager.Set(null, new TestData()));
        }

        [Fact]
        public void Set_WhenKeyIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sessionManager.Set("", new TestData()));
        }

        [Fact]
        public void Set_WhenCalled_SetsSerializedDataToSession()
        {
            // Arrange
            var data = new TestData { Property1 = "value1", Property2 = 2 };
            var sessionData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));

            _httpContextAccessorMock.Setup(x => x.HttpContext.Session.Set("test-key", sessionData))
                                    .Verifiable();

            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act
            sessionManager.Set("test-key", data);

            // Assert
            _httpContextAccessorMock.Verify();
        }

        [Fact]
        public void Remove_WhenKeyIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => sessionManager.Remove(null));
        }

        [Fact]
        public void Remove_WhenKeyIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var sessionManager = new SessionManager(_httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sessionManager.Remove(""));
        }
    }
}