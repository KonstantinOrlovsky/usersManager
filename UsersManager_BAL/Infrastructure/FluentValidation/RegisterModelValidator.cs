using FluentValidation;
using UsersManager_BAL.Models;

namespace UsersManager_BAL.Infrastructure.FluentValidation
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(user => user.Name).Length(5, 70);
            RuleFor(user => user.ConfirmPassword).Length(5, 70).Equal(user => user.Password);
            RuleFor(user => user.Password).MaximumLength(50);
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
        }
    }
}