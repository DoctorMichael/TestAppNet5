using FluentValidation;
using TestApp.DTOs;

namespace TestApp.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEqual("").WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.Password).NotNull().NotEqual("").WithMessage("{PropertyName} should be not empty.");
        }
    }
}
