namespace ExpenseTrackerAPI.API.Models;

public class UpdateProfileModel
{
    public required string Username { get; set; }
    public required string Email { get; set; }
}

public class ChangePasswordModel
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}