namespace ExpenseTrackerAPI.API.Models;

public class RegisterModel
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class ResetPasswordModel
{
    public required string Email { get; set; }
}