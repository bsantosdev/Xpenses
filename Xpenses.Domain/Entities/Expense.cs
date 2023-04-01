namespace Xpenses.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
        public Guid Category { get; set; }
        public List<ExpenseTag> Tags { get; set; } = null!;
    }
}
