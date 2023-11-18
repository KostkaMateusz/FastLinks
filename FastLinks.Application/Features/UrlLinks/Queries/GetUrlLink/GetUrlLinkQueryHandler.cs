using AutoMapper;
using FastLinks.Application.Contracts.Persistence;
using FastLinks.Domain.Entities;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;

public class GetUrlLinkAddressQueryHandler : IRequestHandler<GetUrlLinkAddressQuery, string>
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public GetUrlLinkAddressQueryHandler(IMapper mapper, IUrlLinkRepository urlLinkRepository)
    {
        _mapper = mapper;
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<string> Handle(GetUrlLinkAddressQuery request, CancellationToken cancellationToken)
    {
        var urlLinkDetails = await _urlLinkRepository.GetByIdAsync(request.ShortUrl);

        if(urlLinkDetails is null)
            throw new Exceptions.NotFoundException(nameof(UrlLink),request.ShortUrl);

        if (urlLinkDetails.ExpirationDate < DateTime.UtcNow)
            throw new Exceptions.LinkExpiredException();

        urlLinkDetails.NumberOfEntries++;

        await _urlLinkRepository.UpdateAsync(urlLinkDetails);

        return urlLinkDetails.UrlAddress;
    }
}