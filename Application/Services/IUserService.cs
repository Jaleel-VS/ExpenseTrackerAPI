using ExpenseTrackerAPI.Application.DTOs;

namespace ExpenseTrackerAPI.Application.Services;

public interface IUserService
{
    Task<UserDto> GetProfileAsync(Guid userId);
    Task UpdateProfileAsync(Guid userId, string username, string email);
    Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}