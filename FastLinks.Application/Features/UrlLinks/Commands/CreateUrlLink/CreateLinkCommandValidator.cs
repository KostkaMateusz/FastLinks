using FluentValidation;

namespace FastLinks.Application.Features.UrlLinks.Commands.CreateUrlLink;

public class CreateLinkCommandValidator : AbstractValidator<CreateLinkCommand>
{
    public CreateLinkCommandValidator()
    {
        RuleFor(p => p.UrlAddress)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull()
          .MaximumLength(50).WithMessage("{PropertyName} must not exceed 100 characters.")
          .MinimumLength(3).WithMessage("{PropertyName} must contain at least 3 characters.");
    }
}
