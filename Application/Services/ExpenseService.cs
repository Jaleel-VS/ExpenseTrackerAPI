using ExpenseTrackerAPI.Application.DTOs;
using ExpenseTrackerAPI.Domain.Entities;
using ExpenseTrackerAPI.Domain.Repositories;
using ExpenseTrackerAPI.Application.Common.Exceptions;
using ExpenseTrackerAPI.ApplicationAPI.Common.Exceptions;

namespace ExpenseTrackerAPI.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ExpenseService(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository)
    {
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ExpenseDto> CreateExpenseAsync(Guid userId, string description, decimal amount, DateTime date, Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.UserId != userId)
        {
            throw new NotFoundException(nameof(Category), categoryId);
        }

        var expense = new Expense
        {
            UserId = userId,
            Description = description,
            Amount = amount,
            Date = date,
            CategoryId = categoryId
        };

        await _expenseRepository.CreateAsync(expense);

        return new ExpenseDto
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            Date = expense.Date,
            CategoryId = expense.CategoryId,
            CategoryName = category.Name
        };
    }

    public async Task<IEnumerable<ExpenseDto>> GetExpensesAsync(Guid userId)
    {
        var expenses = await _expenseRepository.GetByUserIdAsync(userId);
        return expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Description = e.Description,
            Amount = e.Amount,
            Date = e.Date,
            CategoryId = e.CategoryId,
            CategoryName = e.Category.Name
        });
    }

    public async Task<ExpenseDto> GetExpenseByIdAsync(Guid userId, Guid expenseId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null || expense.UserId != userId)
        {
            throw new NotFoundException(nameof(Expense), expenseId);
        }

        return new ExpenseDto
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            Date = expense.Date,
            CategoryId = expense.CategoryId,
            CategoryName = expense.Category.Name
        };
    }

    public async Task UpdateExpenseAsync(Guid userId, Guid expenseId, string description, decimal amount, DateTime date, Guid categoryId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null || expense.UserId != userId)
        {
            throw new NotFoundException(nameof(Expense), expenseId);
        }

        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.UserId != userId)
        {
            throw new NotFoundException(nameof(Category), categoryId);
        }

        expense.Description = description;
        expense.Amount = amount;
        expense.Date = date;
        expense.CategoryId = categoryId;

        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task DeleteExpenseAsync(Guid userId, Guid expenseId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null || expense.UserId != userId)
        {
            throw new NotFoundException(nameof(Expense), expenseId);
        }

        await _expenseRepository.DeleteAsync(expenseId);
    }
}