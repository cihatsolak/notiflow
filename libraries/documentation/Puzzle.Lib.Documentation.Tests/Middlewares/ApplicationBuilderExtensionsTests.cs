namespace Puzzle.Lib.Documentation.Tests.Middlewares
{
    public class ApplicationBuilderExtensionsTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly Mock<IOptions<SwaggerSetting>> _optionsMock;
        private readonly Mock<IApplicationBuilder> _appBuilderMock;
        private readonly Mock<IApplicationBuilder> _reDocBuilderMock;

        public ApplicationBuilderExtensionsTests()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _optionsMock = new Mock<IOptions<SwaggerSetting>>();
            _appBuilderMock = new Mock<IApplicationBuilder>();
            _reDocBuilderMock = new Mock<IApplicationBuilder>();
        }

        [Fact]
        public void UseSwaggerDoc_Should_Not_Use_Swagger_In_Production_Environment()
        {
            _webHostEnvironmentMock.Setup(env => env.IsProduction()).Returns(true);

            IApplicationBuilder appBuilder = _appBuilderMock.Object;
            appBuilder.ApplicationServices = _serviceProviderMock.Object;
            _serviceProviderMock.Setup(sp => sp.GetRequiredService<IWebHostEnvironment>()).Returns(_webHostEnvironmentMock.Object);

            appBuilder.UseSwaggerDoc();

            _appBuilderMock.Verify(ab => ab.UseSwagger(It.IsAny<Action<SwaggerOptions>>()), Times.Never);
            _appBuilderMock.Verify(ab => ab.UseSwaggerUI(It.IsAny<Action<SwaggerUIOptions>>()), Times.Never);
        }

        [Fact]
        public void UseSwaggerDoc_Should_Use_Swagger_In_Development_Environment()
        {
            _webHostEnvironmentMock.Setup(env => env.IsProduction()).Returns(false);

            IApplicationBuilder appBuilder = _appBuilderMock.Object;
            appBuilder.ApplicationServices = _serviceProviderMock.Object;
            _serviceProviderMock.Setup(sp => sp.GetRequiredService<IWebHostEnvironment>()).Returns(_webHostEnvironmentMock.Object);

            SwaggerSetting swaggerSetting = new()
            {
                Description = "",
                Title = "",
                Version = ""
            };

            _optionsMock.Setup(op => op.Value).Returns(swaggerSetting);
            _serviceProviderMock.Setup(sp => sp.GetRequiredService<IOptions<SwaggerSetting>>()).Returns(_optionsMock.Object);

            appBuilder.UseSwaggerDoc();

            _appBuilderMock.Verify(ab => ab.UseSwagger(It.IsAny<Action<SwaggerOptions>>()), Times.Once);
            _appBuilderMock.Verify(ab => ab.UseSwaggerUI(It.IsAny<Action<SwaggerUIOptions>>()), Times.Once);
        }
    }
}
