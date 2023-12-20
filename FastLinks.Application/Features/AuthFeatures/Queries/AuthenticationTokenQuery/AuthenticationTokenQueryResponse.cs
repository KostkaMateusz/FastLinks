namespace FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;

public class AuthenticationTokenQueryResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public AuthenticationTokenQueryResponse() { }

    public AuthenticationTokenQueryResponse(Guid Id,string Email,string Token)
    {
        this.Id = Id;
        this.Email = Email;
        this.Token = Token;        
    }
}
