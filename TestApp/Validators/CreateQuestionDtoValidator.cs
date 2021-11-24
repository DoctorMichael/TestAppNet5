using FluentValidation;
using TestApp.BusinessLogic.Validators;
using TestApp.DTOs;

namespace TestApp.Validators
{
    public class CreateQuestionDtoValidator : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionDtoValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty().MaximumLength(100);
            RuleFor(x => x.CreateAnswerDtos).NotEmpty();
            RuleForEach(x => x.CreateAnswerDtos).SetValidator(new CreateAnswerDtoValidator());
        }
    }
}
