using ExpenseTrackerAPI.Domain.Entities;

namespace ExpenseTrackerAPI.Domain.Repositories;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(Guid id);
    Task<IEnumerable<Expense>> GetByUserIdAsync(Guid userId);
    Task<Expense> CreateAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Guid id);
}