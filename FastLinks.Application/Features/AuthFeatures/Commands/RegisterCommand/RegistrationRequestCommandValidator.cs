using FluentValidation;

namespace FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;

internal class RegistrationRequestCommandValidator : AbstractValidator<RegistrationRequestCommand>
{
    public RegistrationRequestCommandValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull().WithMessage("{PropertyName} can not be empty")
            .EmailAddress().WithMessage("{PropertyName} must be an email");

        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull().WithMessage("{PropertyName} can not be empty")
            .MinimumLength(6).WithMessage("{PropertyName} must be minimum 6 characters");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull().WithMessage("{PropertyName} can not be empty")
            .MinimumLength(6).WithMessage("{PropertyName} must be minimum 6 characters"); ;
    }
}