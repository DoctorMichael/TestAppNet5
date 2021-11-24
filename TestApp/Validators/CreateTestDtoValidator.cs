using FluentValidation;
using TestApp.DTOs;

namespace TestApp.Validators
{
    public class CreateTestDtoValidator : AbstractValidator<CreateTestDto>
    {
        public CreateTestDtoValidator()
        {
            RuleFor(x => x.TestName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.QuestionIds).NotEmpty();
            RuleForEach(x => x.QuestionIds).GreaterThan(0); 
        }
    }
}
