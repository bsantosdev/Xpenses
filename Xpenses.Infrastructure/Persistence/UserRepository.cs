using Xpenses.Application.Common.Interfaces.Persistence;
using Xpenses.Domain.Entities;

namespace Xpenses.Infrastructure.Persistence;


public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(XpensesDbContext context)
        : base(context) { }

    public User? GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}
