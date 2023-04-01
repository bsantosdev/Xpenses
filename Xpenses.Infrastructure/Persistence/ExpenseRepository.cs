using Xpenses.Application.Common.Interfaces.Persistence;
using Xpenses.Domain.Entities;

namespace Xpenses.Infrastructure.Persistence
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(XpensesDbContext context)
            : base(context) { }
    }
}