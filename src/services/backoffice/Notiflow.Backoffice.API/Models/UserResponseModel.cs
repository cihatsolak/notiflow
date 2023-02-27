using FluentValidation;
using Notiflow.Lib.Validation.RuleBuilders;

namespace Notiflow.Backoffice.API.Models
{
    public class UserResponseModel
    {
        public string Name { get; set; }
    }

    public class UserValidator : AbstractValidator<UserResponseModel>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).CreditOrDebitCard("");
        }
    }
}
