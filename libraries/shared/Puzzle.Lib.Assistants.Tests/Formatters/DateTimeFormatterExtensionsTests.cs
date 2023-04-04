namespace Puzzle.Lib.Assistants.Tests.Formatters
{
    public sealed class DateTimeFormatterExtensionsTests
    {
        [Fact]
        public void ToDateTimeFormat_ReturnsExpectedResult()
        {
            // Arrange
            DateTime dateTime = new(2022, 4, 5, 15, 30, 0);
            string expected = "05/04/2022 15:30";

            // Act
            string actual = dateTime.ToDateTimeFormat();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToDateFormat_ReturnsExpectedResult()
        {
            // Arrange
            DateTime dateTime = new(2022, 4, 5);
            string expected = "05/04/2022";

            // Act
            string actual = dateTime.ToDateFormat();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToHourFormat_ReturnsExpectedResult()
        {
            // Arrange
            DateTime dateTime = new(2022, 4, 5, 15, 30, 0);
            string expected = "15:30";

            // Act
            string actual = dateTime.ToHourFormat();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
