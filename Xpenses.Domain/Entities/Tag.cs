namespace Xpenses.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public List<ExpenseTag> ExpenseTags { get; set; } = null!;
    }
}