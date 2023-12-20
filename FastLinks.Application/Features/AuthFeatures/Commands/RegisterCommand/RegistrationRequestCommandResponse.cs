namespace FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;

public class RegistrationRequestCommandResponse
{
    public Guid UserId { get; set; }

    public RegistrationRequestCommandResponse() { }

    public RegistrationRequestCommandResponse(Guid UserId)
    {
        this.UserId = UserId;
    }
}
