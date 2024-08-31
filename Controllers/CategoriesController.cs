using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerAPI.Application.Services;
using ExpenseTrackerAPI.API.Models;
using System.Security.Claims;

namespace ExpenseTrackerAPI.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var category = await _categoryService.CreateCategoryAsync(userId, model.Name);
        return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var categories = await _categoryService.GetCategoriesAsync(userId);
        return Ok(categories);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryModel model)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _categoryService.UpdateCategoryAsync(userId, id, model.Name);
        return Ok(new { Message = "Category updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _categoryService.DeleteCategoryAsync(userId, id);
        return Ok(new { Message = "Category deleted successfully" });
    }
}