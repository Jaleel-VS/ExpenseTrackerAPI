using ExpenseTrackerAPI.Application.DTOs;
using ExpenseTrackerAPI.Domain.Entities;
using ExpenseTrackerAPI.Domain.Repositories;
using ExpenseTrackerAPI.ApplicationAPI.Common.Exceptions;

namespace ExpenseTrackerAPI.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> CreateCategoryAsync(Guid userId, string name)
    {
        var category = new Category
        {
            UserId = userId,
            Name = name
        };

        await _categoryRepository.CreateAsync(category);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid userId)
    {
        var categories = await _categoryRepository.GetByUserIdAsync(userId);
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task UpdateCategoryAsync(Guid userId, Guid categoryId, string name)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.UserId != userId)
        {
            throw new NotFoundException(nameof(Category), categoryId);
        }

        category.Name = name;
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(Guid userId, Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.UserId != userId)
        {
            throw new NotFoundException(nameof(Category), categoryId);
        }

        await _categoryRepository.DeleteAsync(categoryId);
    }
}