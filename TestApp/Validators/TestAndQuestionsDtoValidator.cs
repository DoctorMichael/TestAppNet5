using FluentValidation;
using TestApp.DTOs;

namespace TestApp.Validators
{
    public class TestAndQuestionsDtoValidator : AbstractValidator<TestWithQuestionsDto>
    {
        public TestAndQuestionsDtoValidator()
        {
            RuleFor(x => x.TestId).GreaterThan(0);
            RuleFor(x => x.QuestionIds).NotEmpty();
            RuleForEach(x => x.QuestionIds).GreaterThan(0);
        }
    }
}
