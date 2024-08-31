using ExpenseTrackerAPI.Application.DTOs;

namespace ExpenseTrackerAPI.Application.Services;

public interface IExpenseService
{
    Task<ExpenseDto> CreateExpenseAsync(Guid userId, string description, decimal amount, DateTime date, Guid categoryId);
    Task<IEnumerable<ExpenseDto>> GetExpensesAsync(Guid userId);
    Task<ExpenseDto> GetExpenseByIdAsync(Guid userId, Guid expenseId);
    Task UpdateExpenseAsync(Guid userId, Guid expenseId, string description, decimal amount, DateTime date, Guid categoryId);
    Task DeleteExpenseAsync(Guid userId, Guid expenseId);
}