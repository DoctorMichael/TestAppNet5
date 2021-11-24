using FluentValidation;
using TestApp.DTOs;

namespace TestApp.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().Length(4, 20);
        }
    }
}
