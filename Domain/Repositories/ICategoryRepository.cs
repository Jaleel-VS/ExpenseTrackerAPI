using ExpenseTrackerAPI.Domain.Entities;

namespace ExpenseTrackerAPI.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId);
    Task<Category> CreateAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Guid id);
}