using FluentValidation;
using FluentValidation.Results;

namespace TestApp.Validators
{
    public class NotNullValidator : AbstractValidator<object>
    {
        public NotNullValidator() { } 

        protected override bool PreValidate(ValidationContext<object> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.GetType().Name, "The validated object is null."));
                return false;
            }
            return base.PreValidate(context, result);
        }
    }
}
