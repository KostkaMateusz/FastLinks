using AutoMapper;
using FastLinks.Application.Contracts.Persistence;
using FastLinks.Domain.Entities;
using MediatR;
using System.Text;

namespace FastLinks.Application.Features.UrlLinks.Commands.CreateUrlLink;

public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, CreateLinkCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public CreateLinkCommandHandler(IMapper mapper, IUrlLinkRepository urlLinkRepository)
    {
        _mapper = mapper;
        _urlLinkRepository = urlLinkRepository;
    }

    private static string GetUniqueString(int string_length)
    {
        var sb = new StringBuilder();

        int numGuidsToConcat = (((string_length - 1) / 32) + 1);

        for (int i = 1; i <= numGuidsToConcat; i++)
            sb.Append(Guid.NewGuid().ToString("N"));

        return sb.ToString(0, string_length);
    }

    public async Task<CreateLinkCommandResponse> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLinkCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var urlLink = _mapper.Map<UrlLink>(request);

        urlLink.ShortUrlAddress = GetUniqueString(4);

        urlLink = await _urlLinkRepository.AddAsync(urlLink);

        return _mapper.Map<CreateLinkCommandResponse>(urlLink);
    }
}