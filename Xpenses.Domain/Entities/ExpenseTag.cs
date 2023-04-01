namespace Xpenses.Domain.Entities
{
    public class ExpenseTag
    {
        public Guid ExpenseId { get; set; }
        public Expense Expense { get; set; } = null!;
        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}