namespace ExpenseTrackerAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; } // Foreign Key
        public Category? Category { get; set; } // Navigation Property
    }
}
