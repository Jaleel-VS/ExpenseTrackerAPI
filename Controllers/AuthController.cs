using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.Application.Services;
using ExpenseTrackerAPI.Domain.Entities;
using ExpenseTrackerAPI.API.Models;

namespace ExpenseTrackerAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var user = await _authService.RegisterAsync(model.Username, model.Email, model.Password);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var token = await _authService.LoginAsync(model.Username, model.Password);
        return Ok(new { Token = token });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // JWT doesn't require server-side logout, but we'll keep the endpoint for consistency
        return Ok(new { Message = "Logged out successfully" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        await _authService.ResetPasswordAsync(model.Email);
        return Ok(new { Message = "Password reset email sent" });
    }
}