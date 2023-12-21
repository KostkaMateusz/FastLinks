using FastLinks.Application.Contracts.Auth;
using FastLinks.Application.Exceptions;
using FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;
using FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;
using FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;
using FastLinks.Identity.Entities;
using FastLinks.Identity.Models;
using FastLinks.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FastLinks.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly JwtSettings _jwtSettings;
    private readonly IUsersRepository _usersRepository;

    public AuthenticationService(IUsersRepository usersRepository, IPasswordHasher<ApplicationUser> passwordHasher, IOptions<JwtSettings> jwtSettings)
    {
        _passwordHasher = passwordHasher;
        _jwtSettings = jwtSettings.Value;
        _usersRepository = usersRepository;
    }

    public async Task<AuthenticationTokenQueryResponse> AuthenticateAsync(AuthenticationTokenQuery request)
    {
        if (request.Email is null)
            throw new BadRequestException("Invalid User Name or Password");

        if (request.Password is null)
            throw new BadRequestException("Invalid User Name or Password");

        var user = await _usersRepository.GetApplicationUserByEmail(request.Email);

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

        return new AuthenticationTokenQueryResponse(user.UserId, user.Email, tokenString);
    }

    public async Task<RegistrationRequestCommandResponse> RegisterAsync(RegistrationRequestCommand request)
    {
        var userWithMailExist = await _usersRepository.ApplicationUserWithEmailExist(request.Email);

        if (userWithMailExist is true)
            throw new BadRequestException("Email is already taken");

        var newUser = new ApplicationUser()
        {
            Email = request.Email
        };

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);

        var newUserId = await _usersRepository.SaveNewUser(newUser);

        return new RegistrationRequestCommandResponse(newUserId);
    }

    public async Task<bool> DeleteUserAsync(DeleteUserCommand request)
    {
        var user = await _usersRepository.GetApplicationUserById(request.UserId);

        if (user is null)
            throw new BadRequestException("User do not exist");

        await _usersRepository.DeleteUser(user);

        return true;
    }
}