using AutoMapper;
using FastLinks.Application.Contracts.Persistence;
using FastLinks.Domain.Entities;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetails;

public class GetUrlLinkDetailsQueryHandler : IRequestHandler<GetUrlLinkDetailsQuery, GetUrlLinkDetailsQueryVm>
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public GetUrlLinkDetailsQueryHandler(IMapper mapper, IUrlLinkRepository urlLinkRepository)
    {
        _mapper = mapper;
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<GetUrlLinkDetailsQueryVm> Handle(GetUrlLinkDetailsQuery request, CancellationToken cancellationToken)
    {
        var urlLinkDetails = await _urlLinkRepository.GetByIdAsync(request.ShortUrl);

        if(urlLinkDetails is null)
            throw new Exceptions.NotFoundException(nameof(UrlLink),request.ShortUrl);

        if (urlLinkDetails.ExpirationDate < DateTime.UtcNow)
            throw new Exceptions.LinkExpiredException();

        return _mapper.Map<GetUrlLinkDetailsQueryVm>(urlLinkDetails);
    }
}
