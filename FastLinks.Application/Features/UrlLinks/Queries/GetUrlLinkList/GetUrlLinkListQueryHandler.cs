using AutoMapper;
using FastLinks.Application.Contracts.Identity;
using FastLinks.Application.Contracts.Persistence;
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkList;

public class GetUrlLinkListQueryHandler : IRequestHandler<GetUrlLinkListByUserQuery, IReadOnlyList<GetUrlLinkListQueryVm>>
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;
    private readonly IUserService _userService;

    public GetUrlLinkListQueryHandler(IMapper mapper,IUrlLinkRepository urlLinkRepository, IUserService userService)
    {
        _mapper = mapper;
        _urlLinkRepository = urlLinkRepository;
        _userService = userService;
    }

    public async Task<IReadOnlyList<GetUrlLinkListQueryVm>> Handle(GetUrlLinkListByUserQuery request, CancellationToken cancellationToken)
    {       
        var urlLinkList=await _urlLinkRepository.ListAllUserUrlLinksAsync(request.UserId);

        return _mapper.Map<IReadOnlyList<GetUrlLinkListQueryVm>>(urlLinkList);
    }
}