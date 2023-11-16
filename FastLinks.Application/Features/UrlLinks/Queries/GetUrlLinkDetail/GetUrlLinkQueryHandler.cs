using AutoMapper;
using FastLinks.Application.Contracts.Persistence;
using FastLinks.Domain.Entities;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetails;

public class GetUrlLinkQueryHandler : IRequestHandler<GetUrlLinkQuery, UrlLinkAddressVm>
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public GetUrlLinkQueryHandler(IMapper mapper, IUrlLinkRepository urlLinkRepository)
    {
        _mapper = mapper;
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<UrlLinkAddressVm> Handle(GetUrlLinkQuery request, CancellationToken cancellationToken)
    {
        var urlLinkDetails = await _urlLinkRepository.GetByIdAsync(request.ShortUrl);

        if(urlLinkDetails is null)
            throw new Exceptions.NotFoundException(nameof(UrlLink),request.ShortUrl);

        if (urlLinkDetails.ExpirationDate > DateTime.UtcNow)
            throw new Exceptions.LinkExpiredException();

        urlLinkDetails.NumberOfEntries++;

        await _urlLinkRepository.UpdateAsync(urlLinkDetails);

        return _mapper.Map<UrlLinkAddressVm>(urlLinkDetails);
    }
}
