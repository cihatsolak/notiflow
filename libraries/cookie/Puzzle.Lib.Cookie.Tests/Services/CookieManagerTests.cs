namespace Puzzle.Lib.Cookie.Tests.Services
{
    public class CookieManagerTests
    {
        [Fact]
        public void Get_ShouldReturnDefault_WhenKeyDoesNotExist()
        {
            // Arrange
            var key = "non-existing-key";
            var httpContext = new DefaultHttpContext();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(httpContext);
            var cookieManager = new CookieManager(httpContextAccessorMock.Object);

            // Act
            var result = cookieManager.Get<string>(key);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Set_Should_Throw_ArgumentNullException_When_Value_Is_Null()
        {
            // Arrange
            var key = "test-key";
            string value = null;
            var httpContext = new DefaultHttpContext();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(httpContext);
            var cookieManager = new CookieManager(httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => cookieManager.Set<string>(key, value));
        }

        [Fact]
        public void Set_Should_Throw_ArgumentException_When_Key_Is_NullOrEmpty()
        {
            // Arrange
            var key = "";
            var value = "test-value";
            var httpContext = new DefaultHttpContext();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(httpContext);
            var cookieManager = new CookieManager(httpContextAccessorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => cookieManager.Set<string>(key, value));
        }

        //[Fact]
        //public void Set_Should_Set_Cookie_With_Expected_Value()
        //{
        //    // Arrange
        //    var key = "test-key";
        //    var value = "test-value";
        //    var httpContext = new DefaultHttpContext();
        //    var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        //    httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(httpContext);
        //    var cookieManager = new CookieManager(httpContextAccessorMock.Object);

        //    // Act
        //    cookieManager.Set<string>(key, value);

        //    // Assert
        //    Assert.Equal(value, httpContext.Response.Cookies[key]);
        //}

        //[Fact]
        //public void Set_Should_Set_Cookie_With_Expected_Expiration_Date()
        //{
        //    // Arrangeca
        //    var key = "test-key";
        //    var value = "test-value";
        //    var expireDate = new DateTime(2023, 3, 31);
        //    var httpContext = new DefaultHttpContext();
        //    var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        //    httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(httpContext);
        //    var cookieManager = new CookieManager(httpContextAccessorMock.Object);

        //    // Act
        //    cookieManager.Set<string>(key, value, expireDate);

        //    // Assert
        //    Assert.Equal(expireDate, httpContext.Response.Cookies[key].Expires);
        //}

        [Fact]
        public void Remove_Should_Remove_Cookie_From_Response()
        {
            // Arrange
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpResponse = new Mock<HttpResponse>();
            mockHttpContextAccessor.SetupGet(x => x.HttpContext.Response).Returns(mockHttpResponse.Object);
            var cookieManager = new CookieManager(mockHttpContextAccessor.Object);

            string key = "test-key";
            mockHttpResponse.Setup(x => x.Cookies.Delete(key));

            // Act
            cookieManager.Remove(key);

            // Assert
            mockHttpResponse.Verify(x => x.Cookies.Delete(key), Times.Once);
        }

        [Fact]
        public void Remove_Should_Throw_ArgumentException_When_Key_Is_NullOrEmpty()
        {
            // Arrange
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var cookieManager = new CookieManager(mockHttpContextAccessor.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => cookieManager.Remove(null));
            Assert.Throws<ArgumentException>(() => cookieManager.Remove(string.Empty));
        }

        [Fact]
        public void Get_Should_Return_Default_When_Key_Not_Found()
        {
            // Arrange
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpRequest = new Mock<HttpRequest>();
            var mockHttpResponse = new Mock<HttpResponse>();
            mockHttpContextAccessor.SetupGet(x => x.HttpContext.Request).Returns(mockHttpRequest.Object);
            mockHttpContextAccessor.SetupGet(x => x.HttpContext.Response).Returns(mockHttpResponse.Object);
            var cookieManager = new CookieManager(mockHttpContextAccessor.Object);

            string key = "test-key";
            mockHttpRequest.Setup(x => x.Cookies[key]).Returns((string)null);

            // Act
            var result = cookieManager.Get<string>(key);

            // Assert
            Assert.Null(result);
        }

        //    [Fact]
        //    public void Get_Should_Deserialize_CookieValue_To_Generic_Type()
        //    {
        //        // Arrange
        //        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        //        var mockHttpRequest = new Mock<HttpRequest>();
        //        var mockHttpResponse = new Mock<HttpResponse>();
        //        mockHttpContextAccessor.SetupGet(x => x.HttpContext.Request).Returns(mockHttpRequest.Object);
        //        mockHttpContextAccessor.SetupGet(x => x.HttpContext.Response).Returns(mockHttpResponse.Object);
        //        var cookieManager = new CookieManager(mockHttpContextAccessor.Object);

        //        string key = "test-key";
        //        string cookieValue = "{\"Id\":1,\"Name\":\"Test\",\"IsAdmin\":true}";
        //        mockHttpRequest.Setup(x => x.Cookies[key]).Returns(cookieValue);

        //        // Act
        //        var result = cookieManager.Get<User>(key);

        //        // Assert
        //        Assert.NotNull(result);
        //        Assert.Equal(1, result.Id);
        //        Assert.Equal("Test", result.Name);
        //        Assert.True(result.IsAdmin);
        //    }

        //    [Fact]
        //    public void Set_Should_Set_CookieValue_To_Response()
        //    {
        //        // Arrange
        //        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        //        var mockHttpResponse = new Mock<HttpResponse>();
        //        mockHttpContextAccessor.SetupGet(x => x.HttpContext.Response).Returns(mockHttpResponse.Object);
        //        var cookieManager = new CookieManager(mockHttpContextAccessor.Object);

        //        string key = "test-key";
        //        var data = new User { Id = 1, Name = "Test", IsAdmin = true };
        //        var expectedCookieValue = "{\"Id\":1,\"Name\":\"Test\",\"IsAdmin\":true}";

        //        // Act
        //        cookieManager.Set(key, data);

        //        // Assert
        //        mockHttpResponse.Verify(x => x.Cookies.Append(key, expectedCookieValue, It.IsAny<CookieOptions>()), Times.Once);
        //    }

        //    [Fact]
        //    public void Set_Should_Throw_ArgumentException_When_Key_Is_NullOrEmpty()
        //    {
        //        // Arrange
        //        var httpContextAccessor = new Mock<IHttpContextAccessor>().Object;
        //        var cookieManager = new CookieManager(httpContextAccessor);

        //        // Act and Assert
        //        Assert.Throws<ArgumentException>(() => cookieManager.Set<object>("", new object()));
        //    }

        //    [Fact]
        //    public void Set_Should_Throw_ArgumentNullException_When_Value_Is_Null()
        //    {
        //        // Arrange
        //        var httpContextAccessor = new Mock<IHttpContextAccessor>().Object;
        //        var cookieManager = new CookieManager(httpContextAccessor);

        //        // Act and Assert
        //        Assert.Throws<ArgumentNullException>(() => cookieManager.Set<object>("key", null));
        //    }
    }
}

