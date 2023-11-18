using AutoMapper;
using FastLinks.Application.Features.UrlLinks.Commands.CreateUrlLink;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetails;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkList;

namespace FastLinks.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {        
        CreateMap<CreateLinkCommand, Domain.Entities.UrlLink>();
        CreateMap<Domain.Entities.UrlLink, CreateLinkCommandResponse>();
        
        CreateMap<Domain.Entities.UrlLink, GetUrlLinkDetailsQueryVm>();

        CreateMap<Domain.Entities.UrlLink, GetUrlLinkListQueryVm>();        
    }
}
