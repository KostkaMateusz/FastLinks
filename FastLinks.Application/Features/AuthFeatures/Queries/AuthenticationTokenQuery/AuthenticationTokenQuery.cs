using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;

public class AuthenticationTokenQuery : IRequest<AuthenticationTokenQueryResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}