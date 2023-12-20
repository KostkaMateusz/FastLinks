
using FluentValidation;

namespace FastLinks.Application.Features.UrlLinks.Commands.UpdateUrlLink;

public class UpdateLinkCommandValidator : AbstractValidator<UpdateLinkCommand>
{
    public UpdateLinkCommandValidator()
    {
        RuleFor(ul => ul.ExpirationDate)
            .NotEmpty()
            .NotNull().WithMessage("{PropertyName} can not be null")
            .Must(DateTimeNotInThePast).WithMessage("{PropertyName} must be in the future");
    }

    private bool DateTimeNotInThePast(DateTime expirationDate)
    {
        var dateIsGreaterThanDotady = DateTime.Compare(expirationDate, DateTime.UtcNow);

        if (dateIsGreaterThanDotady >= 0)
            return true;

        return false;
    }
}