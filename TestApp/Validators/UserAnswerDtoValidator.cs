using FluentValidation;
using TestApp.DTOs;

namespace TestApp.Validators
{
    public class UserAnswerDtoValidator : AbstractValidator<UserAnswerDto>
    {
        public UserAnswerDtoValidator()
        {
            RuleFor(x => x.UserID).GreaterThan(0);
            RuleFor(x => x.TestID).GreaterThan(0);
            RuleFor(x => x.AnswerID).GreaterThan(0);
        }
    }
}
