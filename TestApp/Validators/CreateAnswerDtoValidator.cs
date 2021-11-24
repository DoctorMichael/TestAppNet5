using FluentValidation;
using TestApp.DTOs;

namespace TestApp.BusinessLogic.Validators
{
    public class CreateAnswerDtoValidator : AbstractValidator<CreateAnswerDto>
    {
        public CreateAnswerDtoValidator()
        {
            RuleFor(x => x.AnswerText).NotEmpty().MaximumLength(100);
        }
    }
}
