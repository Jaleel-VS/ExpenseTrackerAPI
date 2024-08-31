using ExpenseTrackerAPI.Application.DTOs;
using ExpenseTrackerAPI.Domain.Repositories;
using ExpenseTrackerAPI.Application.Common.Exceptions;
using BC = BCrypt.Net.BCrypt;
using ExpenseTrackerAPI.ApplicationAPI.Common.Exceptions;

namespace ExpenseTrackerAPI.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetProfileAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(user), userId);
        }

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task UpdateProfileAsync(Guid userId, string username, string email)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(user), userId);
        }

        user.Username = username;
        user.Email = email;

        await _userRepository.UpdateAsync(user);
    }

    public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(user), userId);
        }

        if (!BC.Verify(currentPassword, user.PasswordHash))
        {
            throw new Common.Exceptions.ApplicationException("Current password is incorrect");
        }

        user.PasswordHash = BC.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);
    }
}