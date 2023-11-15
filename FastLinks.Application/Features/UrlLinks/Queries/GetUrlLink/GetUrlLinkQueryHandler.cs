using AutoMapper;
using FastLinks.Application.Contracts.Persistence;
using FastLinks.Domain.Entities;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;

public class GetUrlLinkQueryHandler : IRequestHandler<GetUrlLinkQuery, UrlLinkVm>
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public GetUrlLinkQueryHandler(IMapper mapper, IUrlLinkRepository urlLinkRepository)
    {
        _mapper = mapper;
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<UrlLinkVm> Handle(GetUrlLinkQuery request, CancellationToken cancellationToken)
    {
        var urlLinkDetails = await _urlLinkRepository.GetByIdAsync(request.ShortUrl);

        if(urlLinkDetails is null)
            throw new Exceptions.NotFoundException(nameof(UrlLink),request.ShortUrl);

        urlLinkDetails.NumberOfEntries++;

        await _urlLinkRepository.UpdateAsync(urlLinkDetails);

        return _mapper.Map<UrlLinkVm>(request);
    }
}
