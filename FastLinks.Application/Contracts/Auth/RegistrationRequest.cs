using System.ComponentModel.DataAnnotations;

namespace FastLinks.Application.Contracts.Auth;

public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
