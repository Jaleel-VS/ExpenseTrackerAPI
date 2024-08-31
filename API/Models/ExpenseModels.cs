namespace ExpenseTrackerAPI.API.Models;

public class CreateExpenseModel
{
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid CategoryId { get; set; }
}

public class UpdateExpenseModel
{
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid CategoryId { get; set; }
}