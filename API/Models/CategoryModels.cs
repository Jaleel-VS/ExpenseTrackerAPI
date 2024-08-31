namespace ExpenseTrackerAPI.API.Models;

public class CreateCategoryModel
{
    public required string Name { get; set; }
}

public class UpdateCategoryModel
{
    public required string Name { get; set; }
}