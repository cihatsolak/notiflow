namespace Puzzle.Lib.Assistant.Tests.Formatters
{
    public sealed class PhoneFormatterExtensionsTests
    {
        [Fact]
        public void ToGsmFormat_ThrowsArgumentNullException_WhenPhoneIsNull()
        {
            // Arrange
            string phoneNumber = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => phoneNumber.ToMobilePhoneFormat());
        }

        [Fact]
        public void ToGsmFormat_ThrowsArgumentException_WhenPhoneIsEmpty()
        {
            // Arrange
            string phoneNumber = string.Empty;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => phoneNumber.ToMobilePhoneFormat());
        }

        [Theory]
        [InlineData("5060111111", "0 506 011 11 11")]
        [InlineData("5455555555", "0 545 555 55 55")]
        public void ToGsmFormat_ReturnsExpectedResult(string phone, string expected)
        {
            // Act
            string actual = phone.ToMobilePhoneFormat();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
