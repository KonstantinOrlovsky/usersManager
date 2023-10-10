﻿using FluentValidation;
using UsersManager_BAL.Models.InputModels;

namespace UsersManager_BAL.Infrastructure.FluentValidation
{
    public class AppUserUpdateModelValidator : AbstractValidator<AppUserUpdateModel>
    {
        public AppUserUpdateModelValidator()
        {
            RuleFor(user => user.Id).NotEmpty();
            RuleFor(user => user.Name).Length(5, 70);
            RuleFor(user => user.Password).MaximumLength(50);
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Age).InclusiveBetween(1, 150);
        }
    }
}