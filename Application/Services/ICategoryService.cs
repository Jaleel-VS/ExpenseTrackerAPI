using ExpenseTrackerAPI.Application.DTOs;

namespace ExpenseTrackerAPI.Application.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(Guid userId, string name);
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid userId);
    Task UpdateCategoryAsync(Guid userId, Guid categoryId, string name);
    Task DeleteCategoryAsync(Guid userId, Guid categoryId);
}