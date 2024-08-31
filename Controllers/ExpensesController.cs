using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerAPI.Application.Services;
using ExpenseTrackerAPI.API.Models;
using System.Security.Claims;

namespace ExpenseTrackerAPI.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense(CreateExpenseModel model)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var expense = await _expenseService.CreateExpenseAsync(userId, model.Description, model.Amount, model.Date, model.CategoryId);
        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var expenses = await _expenseService.GetExpensesAsync(userId);
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var expense = await _expenseService.GetExpenseByIdAsync(userId, id);
        return Ok(expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(Guid id, UpdateExpenseModel model)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _expenseService.UpdateExpenseAsync(userId, id, model.Description, model.Amount, model.Date, model.CategoryId);
        return Ok(new { Message = "Expense updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _expenseService.DeleteExpenseAsync(userId, id);
        return Ok(new { Message = "Expense deleted successfully" });
    }
}