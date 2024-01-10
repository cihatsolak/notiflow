using Puzzle.Lib.Http.Infrastructure.Extensions;

namespace Puzzle.Lib.Http.Tests
{
    public class HttpClientExtensionsTests
    {
        [Fact]
        public void CreateCollectionForBearerToken_Should_Return_NameValueCollection_With_Bearer_Token()
        {
            // Arrange
            var token = "abc123";

            // Act
            var result = HttpHeaderExtensions.CreateCollectionForBearerToken(token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal($"Bearer {token}", result[HeaderNames.Authorization]);
        }

        [Fact]
        public void CreateCollectionForBearerToken_ShouldThrowArgumentNullException_WhenTokenIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderExtensions.CreateCollectionForBearerToken(null));
        }

        [Fact]
        public void CreateCollectionForBearerToken_ShouldThrowArgumentException_WhenTokenIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => HttpHeaderExtensions.CreateCollectionForBearerToken(""));
        }

        [Fact]
        public void AddBearerTokenToHeader_Should_Add_Bearer_Token_To_NameValueCollection()
        {
            // Arrange
            var nameValueCollection = new NameValueCollection();
            var token = "abc123";

            // Act
            var result = nameValueCollection.AddBearerToken(token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal($"Bearer {token}", result[HeaderNames.Authorization]);
        }

        [Fact]
        public void AddBearerTokenToHeader_ShouldThrowArgumentNullException_WhenNameValueCollectionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderExtensions.AddBearerToken(null, "token"));
        }

        [Fact]
        public void AddBearerTokenToHeader_ShouldThrowArgumentException_WhenTokenIsEmpty()
        {
            var collection = new NameValueCollection();
            Assert.Throws<ArgumentException>(() => collection.AddBearerToken(""));
        }

        [Fact]
        public void GenerateHeader_Should_Return_NameValueCollection_With_Given_Name_And_Value()
        {
            // Arrange
            var name = "Content-Type";
            var value = "application/json";

            // Act
            var result = HttpHeaderExtensions.Generate(name, value);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(value, result[name]);
        }

        [Fact]
        public void GenerateHeader_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderExtensions.Generate(null, "value"));
        }

        [Fact]
        public void GenerateHeader_ShouldThrowArgumentNullException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderExtensions.Generate("name", null));
        }

        [Fact]
        public void AddHeaderItem_Should_Add_Given_Name_And_Value_To_NameValueCollection()
        {
            // Arrange
            var nameValueCollection = new NameValueCollection();
            var name = "X-Custom-Header";
            var value = "custom value";

            // Act
            nameValueCollection.AddItem(name, value);

            // Assert
            Assert.NotNull(nameValueCollection);
            Assert.Equal(value, nameValueCollection[name]);
        }

        [Fact]
        public void AddHeaderItem_ShouldThrowArgumentNullException_WhenNameValueCollectionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderExtensions.AddItem(null, "name", "value"));
        }

        [Fact]
        public void AddHeaderItem_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
            var collection = new NameValueCollection();
            Assert.Throws<ArgumentException>(() => collection.AddItem(string.Empty, "value"));
        }

        [Fact]
        public void AddHeaderItem_ShouldThrowArgumentException_WhenValueIsEmpty()
        {
            var collection = new NameValueCollection();
            Assert.Throws<ArgumentException>(() => collection.AddItem("name", string.Empty));
        }
    }
}