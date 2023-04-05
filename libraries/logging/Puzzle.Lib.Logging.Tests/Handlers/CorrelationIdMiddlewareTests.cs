namespace Puzzle.Lib.Logging.Tests.Handlers
{
    public class CorrelationIdMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_ShouldAddCorrelationIdHeaderToRequest_WhenHeaderNotExists()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
            var next = new RequestDelegate(context => Task.CompletedTask);
            var middleware = new CorrelationIdMiddleware(next, httpContextAccessorMock.Object);

            // Act
            await middleware.InvokeAsync(httpContext);

            // Assert
            Assert.True(httpContext.Request.Headers.TryGetValue("x-correlation-id", out var values));
            Assert.NotNull(values.First());
        }

        [Fact]
        public async Task InvokeAsync_ShouldAddCorrelationIdHeaderToResponse_WhenHeaderNotExists()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
            var next = new RequestDelegate(context => Task.CompletedTask);
            var middleware = new CorrelationIdMiddleware(next, httpContextAccessorMock.Object);

            // Act
            await middleware.InvokeAsync(httpContext);

            // Assert
            Assert.True(httpContext.Response.Headers.ContainsKey("x-correlation-id"));
            Assert.NotEmpty(httpContext.Response.Headers["x-correlation-id"]);
        }

        [Fact]
        public async Task InvokeAsync_ShouldAddCorrelationIdHeaderToRequestAndResponse_WhenHeaderExists()
        {
            // Arrange
            var correlationId = "test-correlation-id";
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("x-correlation-id", correlationId);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
            var next = new RequestDelegate(context => Task.CompletedTask);
            var middleware = new CorrelationIdMiddleware(next, httpContextAccessorMock.Object);

            // Act
            await middleware.InvokeAsync(httpContext);

            // Assert
            Assert.Equal(correlationId, httpContext.Request.Headers["x-correlation-id"]);
            Assert.Equal(correlationId, httpContext.Response.Headers["x-correlation-id"]);
        }
    }
}
