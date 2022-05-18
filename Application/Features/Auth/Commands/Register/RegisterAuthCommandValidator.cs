using FluentValidation;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterAuthCommandValidator : AbstractValidator<RegisterAuthCommand>
    {
        public RegisterAuthCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100);
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .EmailAddress().WithMessage("{PropertyName} must be valid")
                .MaximumLength(100);
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100);
            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("{PropertyName} can not be empty")
               .MaximumLength(100);
            RuleFor(x => x.ConfirmPassword)
               .NotEmpty().WithMessage("{PropertyName} can not be empty")
               .MaximumLength(100)
               .Equal(x => x.Password).WithMessage("Passwords must be equal");
        }
    }
}
