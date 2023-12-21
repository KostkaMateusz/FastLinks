using FastLinks.Application.Contracts.Auth;
using FastLinks.Application.Contracts.Persistence;
using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    public readonly IAuthenticationService _authenticationService;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public DeleteUserCommandHandler(IAuthenticationService authenticationService, IUrlLinkRepository urlLinkRepository)
    {
        _authenticationService = authenticationService;
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _authenticationService.DeleteUserAsync(request);

        var userLinks = await _urlLinkRepository.ListAllUserUrlLinksAsync(request.UserId);

        await _urlLinkRepository.DeleteLinks(userLinks.ToList());

        return true;
    }
}
