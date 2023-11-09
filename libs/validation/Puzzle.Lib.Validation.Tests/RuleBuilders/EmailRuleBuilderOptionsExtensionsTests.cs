namespace Puzzle.Lib.Validation.Tests.RuleBuilders
{
    public class EmailRuleBuilderOptionsExtensionsTests
    {
        [Fact]
        public void Email_WhenGivenInvalidEmailAddress_ReturnsValidationError()
        {
            // Arrange
            var invalidEmailAddress = "invalid_email_address.com";
            var errorMessage = "Invalid email address";
            var validator = new InlineValidator<TestClass>();
            validator.RuleFor(x => x.Email).Email(errorMessage);

            // Act
            var result = validator.TestValidate(new TestClass { Email = invalidEmailAddress });

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage(errorMessage);
        }

        [Fact]
        public void Email_WhenGivenNullValue_ReturnsValidationError()
        {
            // Arrange
            var errorMessage = "Email is required";
            var validator = new InlineValidator<TestClass>();
            validator.RuleFor(x => x.Email).Email(errorMessage);

            // Act
            var result = validator.TestValidate(new TestClass { Email = null });

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage(errorMessage);
        }

        [Fact]
        public void Email_WhenGivenEmptyValue_ReturnsValidationError()
        {
            // Arrange
            var errorMessage = "Email is required";
            var validator = new InlineValidator<TestClass>();
            validator.RuleFor(x => x.Email).Email(errorMessage);

            // Act
            var result = validator.TestValidate(new TestClass { Email = "" });

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage(errorMessage);
        }

        [Fact]
        public void EmailListWithSemicolon_WhenGivenInvalidEmailListWithTrailingSemicolon_ReturnsValidationError()
        {
            // Arrange
            var invalidEmailList = "email1example.asd;email2@example.com;";
            var errorMessage = "Invalid email address";
            var validator = new InlineValidator<TestClass>();
            //validator.RuleFor(x => x.EmailList).EmailListWithSemicolon(errorMessage);

            // Act
            var result = validator.TestValidate(new TestClass { EmailList = invalidEmailList });

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.EmailList)
                  .WithErrorMessage(errorMessage);
        }

        private class TestClass
        {
            public string Email { get; set; }
            public string EmailList { get; set; }
        }
    }
}