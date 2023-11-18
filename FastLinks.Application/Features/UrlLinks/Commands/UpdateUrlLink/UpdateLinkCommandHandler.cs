using FastLinks.Application.Contracts.Persistence;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Commands.UpdateUrlLink;

public class UpdateLinkCommandHandler : IRequestHandler<UpdateLinkCommand, string>
{
    private readonly IUrlLinkRepository _urlLinkRepository;

    public UpdateLinkCommandHandler(IUrlLinkRepository urlLinkRepository)
    {
        _urlLinkRepository = urlLinkRepository;
    }
    public async Task<string> Handle(UpdateLinkCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLinkCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var urlLink=await _urlLinkRepository.GetByIdAsync(request.ShortUrlAddress);

        if (urlLink is null)
            throw new Exceptions.NotFoundException(nameof(UpdateLinkCommand),request.ShortUrlAddress);

        if (urlLink.UserCreatorId != request.UserId)
            throw new Exceptions.UnauthorisedException();

        urlLink.ExpirationDate = request.ExpirationDate;

        await _urlLinkRepository.UpdateAsync(urlLink);

        return urlLink.ShortUrlAddress;
    }
}
