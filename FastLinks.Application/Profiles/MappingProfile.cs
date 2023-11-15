using AutoMapper;
using FastLinks.Application.Features.UrlLinks.Commands.CreateUrlLink;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;
using FastLinks.Domain.Entities;

namespace FastLinks.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UrlLinkVm,UrlLink>().ReverseMap();
        CreateMap<CreateLinkCommand,UrlLink>().ReverseMap(); 
    }
}
