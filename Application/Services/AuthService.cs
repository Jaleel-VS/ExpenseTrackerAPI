using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseTrackerAPI.Application.DTOs;
using ExpenseTrackerAPI.Domain.Entities;
using ExpenseTrackerAPI.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace ExpenseTrackerAPI.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<UserDto> RegisterAsync(string username, string email, string password)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(username);
        if (existingUser != null)
        {
            throw new ApplicationException("Username already exists");
        }

        existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new ApplicationException("Email already exists");
        }

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = BC.HashPassword(password)
        };

        Console.WriteLine($"Registering user: {username}, Email: {email}, PasswordHash length: {user.PasswordHash.Length}");

        await _userRepository.CreateAsync(user);

        Console.WriteLine($"User registered successfully. ID: {user.Id}");

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        Console.WriteLine($"Login attempt for username: {username}");
        Console.WriteLine($"User found: {user != null}");
        if (user != null)
        {
            Console.WriteLine($"Stored hash length: {user.PasswordHash.Length}");
            Console.WriteLine($"Provided password length: {password.Length}");
            Console.WriteLine($"Password verification result: {BC.Verify(password, user.PasswordHash)}");
        }

        if (user == null || !BC.Verify(password, user.PasswordHash))
        {
            throw new ApplicationException("Invalid username or password");
        }

        var token = GenerateJwtToken(user);
        return token;
    }

    public Task LogoutAsync(Guid userId)
    {
        // JWT doesn't require server-side logout
        return Task.CompletedTask;
    }

    public async Task ResetPasswordAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            throw new ApplicationException("User not found");
        }

        // Here you would typically generate a password reset token and send an email
        // For this example, we'll just log that the password reset was requested
        Console.WriteLine($"Password reset requested for user: {user.Email}");
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}