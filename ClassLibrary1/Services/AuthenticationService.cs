using FastLinks.Identity.Entities;
using FastLinks.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastLinks.Application.Contracts.Auth;
using FastLinks.Application.Exceptions;
using FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;
using FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;

namespace FastLinks.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
    }

    public async Task<AuthenticationTokenQueryResponse> AuthenticateAsync(AuthenticationTokenQuery request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)        
            throw new BadRequestException("Invalid User Name or Password");        

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        if (result.Succeeded is not true)
            throw new BadRequestException("Invalid User Name or Password");


        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthenticationTokenQueryResponse response = new()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

    public async Task<RegistrationRequestCommandResponse> RegisterAsync(RegistrationRequestCommand request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);

        if (existingUser is not null)        
            throw new Exception($"Username '{request.UserName}' already exists.");
        
        var user = new ApplicationUser
        {
            Email = request.Email,           
            UserName = request.UserName,
            EmailConfirmed = true
        };

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail is null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded is true)
            {
                return new RegistrationRequestCommandResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
        else
        {
            throw new Exception($"Email {request.Email} already exists.");
        }
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim("roles", role));
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

        var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );
        
        return jwtSecurityToken;
    }
}
