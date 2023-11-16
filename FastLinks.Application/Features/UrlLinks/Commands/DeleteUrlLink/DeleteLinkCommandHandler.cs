
using FastLinks.Application.Contracts.Persistence;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Commands.DeleteUrlLink;

public class DeleteLinkCommandHandler : IRequestHandler<DeleteLinkCommand>
{
    private readonly IUrlLinkRepository _urlLinkRepository;

    public DeleteLinkCommandHandler(IUrlLinkRepository urlLinkRepository)
    {
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
    {
        var urlLink= await _urlLinkRepository.GetByIdAsync(request.ShortUrlAddress);

        if (urlLink is null)
            throw new Exceptions.NotFoundException(nameof(DeleteLinkCommand), request.ShortUrlAddress);

        if (urlLink.UserCreatorId != request.UserId)
            throw new Exceptions.UnauthorisedException();

        await _urlLinkRepository.DeleteAsync(urlLink);
    }
}
