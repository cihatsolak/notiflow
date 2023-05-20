namespace Puzzle.Lib.Response.Tests
{
    public class MainControllerTests
    {
        [Fact]
        public void MainController_ShouldHaveAttributes()
        {
            // Arrange
            var controllerType = typeof(MainController);
            var expectedAttributes = new List<Type>()
            {
                typeof(ApiControllerAttribute),
                typeof(ProducesAttribute),
                typeof(ConsumesAttribute),
                typeof(ProducesResponseTypeAttribute),
                typeof(ProducesResponseTypeAttribute),
                typeof(ControllerAttribute)
            };

            // Act
            var attributes = controllerType.GetCustomAttributes(true).Select(attributes => attributes.GetType());

            // Assert
            Assert.Equal(expectedAttributes, attributes);
        }
    }
}