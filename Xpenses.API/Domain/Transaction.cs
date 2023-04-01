using Xpenses.Domain.Entities;

namespace Xpenses.API.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public double Value { get; set; }

        public TransactionType TransactionType { get; set; }

        public Category Category { get; set; } 
    }
}
