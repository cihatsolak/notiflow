namespace Puzzle.Lib.Assistant.Tests.Extensions
{
    public class InputExtensionsTests
    {
        [Theory]
        [InlineData("0 (500) 173-00-00", "05001730000")]
        [InlineData("+90 500 1730000", "905001730000")]
        [InlineData("0 500-173-00-00", "05001730000")]
        [InlineData("(500) 1730000", "5001730000")]
        public void ToCleanPhoneNumber_ShouldReturnCleanedPhoneNumber(string input, string expectedOutput)
        {
            // Arrange & Act
            var result = input.ToCleanPhoneNumber();

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void ToCleanPhoneNumber_ShouldThrowException_WhenInputIsNullOrEmpty()
        {
            // Arrange
            string input = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => input.ToCleanPhoneNumber());
            Assert.Equal("Value cannot be null. (Parameter 'phoneNumber')", exception.Message);
        }
    }
}