using FastLinks.Application.Contracts.Auth;
using FastLinks.Application.Exceptions;
using FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;
using FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;
using FastLinks.Identity.Entities;
using FastLinks.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FastLinks.Identity.Services;

public class AuthenticationService : IAuthenticationService
{

    private readonly FastLinksIdentityDbContext _fastLinksIdentityDbContext;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(FastLinksIdentityDbContext fastLinksIdentityDbContext, IPasswordHasher<ApplicationUser> passwordHasher, JwtSettings jwtSettings)
    {
        _fastLinksIdentityDbContext = fastLinksIdentityDbContext;
        _passwordHasher = passwordHasher;
        _jwtSettings = jwtSettings;
    }

    public async Task<AuthenticationTokenQueryResponse> AuthenticateAsync(AuthenticationTokenQuery request)
    {
        var user = await _fastLinksIdentityDbContext.ApplicationUsers.FirstOrDefaultAsync(user=>user.Email==request.Email);

        if (user is null)
            throw new BadRequestException("Invalid User Name or Password");

        if (user.PasswordHash is null)
            throw new BadRequestException("Invalid User Name or Password");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid User Name or Password");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);

        var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Issuer, claims, expires: expires, signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenString = tokenHandler.WriteToken(token);

        return new AuthenticationTokenQueryResponse(user.UserId,user.Email,tokenString);
    }

    public async Task<RegistrationRequestCommandResponse> RegisterAsync(RegistrationRequestCommand request)
    {
        // Check mail in use?

        var newUser = new ApplicationUser()
        {
            Email = request.Email            
        };
        
        newUser.PasswordHash= _passwordHasher.HashPassword(newUser, request.Password);

        await _fastLinksIdentityDbContext.AddAsync(newUser);
        await _fastLinksIdentityDbContext.SaveChangesAsync();

        return new RegistrationRequestCommandResponse(newUser.UserId);
    }
}
