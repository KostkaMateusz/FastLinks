using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;

public class RegistrationRequestCommand : IRequest<RegistrationRequestCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}