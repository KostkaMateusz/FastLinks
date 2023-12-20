using AutoMapper;
using FastLinks.Application.Contracts.Persistence;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkList;

public class GetUrlLinkListQueryHandler : IRequestHandler<GetUrlLinkListByUserQuery, IReadOnlyList<GetUrlLinkListQueryVm>>
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public GetUrlLinkListQueryHandler(IMapper mapper, IUrlLinkRepository urlLinkRepository)
    {
        _mapper = mapper;
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<IReadOnlyList<GetUrlLinkListQueryVm>> Handle(GetUrlLinkListByUserQuery request, CancellationToken cancellationToken)
    {
        var urlLinkList = await _urlLinkRepository.ListAllUserUrlLinksAsync(request.UserId);

        return _mapper.Map<IReadOnlyList<GetUrlLinkListQueryVm>>(urlLinkList);
    }
}