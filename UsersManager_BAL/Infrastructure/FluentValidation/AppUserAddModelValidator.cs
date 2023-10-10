using FluentValidation;
using UsersManager_BAL.Models.InputModels;

namespace UsersManager_BAL.Infrastructure.FluentValidation
{
    public class AppUserAddModelValidator : AbstractValidator<AppUserAddModel>
    {
        public AppUserAddModelValidator()
        {
            RuleFor(user => user.Name).Length(5, 70);
            RuleFor(user => user.Password).MaximumLength(50);
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Age).InclusiveBetween(1, 150);
        }
    }
}