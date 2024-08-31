using ExpenseTrackerAPI.Application.DTOs;

namespace ExpenseTrackerAPI.Application.Services;

public interface IAuthService
{
    Task<UserDto> RegisterAsync(string username, string email, string password);
    Task<string> LoginAsync(string username, string password);
    Task LogoutAsync(Guid userId);
    Task ResetPasswordAsync(string email);
}