using Microsoft.EntityFrameworkCore;
using Xpenses.Domain.Entities;

namespace Xpenses.Infrastructure
{
    public class XpensesDbContext : DbContext
    {
        public XpensesDbContext(DbContextOptions<XpensesDbContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseTag>()
                .HasKey(et => new { et.ExpenseId, et.TagId });

            modelBuilder.Entity<ExpenseTag>()
                .HasOne(et => et.Expense)
                .WithMany(e => e.Tags)
                .HasForeignKey(et => et.ExpenseId);

            modelBuilder.Entity<ExpenseTag>()
                .HasOne(et => et.Tag)
                .WithMany(t => t.ExpenseTags)
                .HasForeignKey(et => et.TagId);
        }
    }
}
