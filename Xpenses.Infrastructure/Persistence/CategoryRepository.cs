using Xpenses.Application.Common.Interfaces.Persistence;
using Xpenses.Domain.Entities;

namespace Xpenses.Infrastructure.Persistence
{

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(XpensesDbContext context)
            : base(context) { }
    }
}
