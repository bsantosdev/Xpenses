using Xpenses.Domain.Entities;

namespace Xpenses.Application.Common.Interfaces.Persistence;

public interface IUserRepository : IRepository<User>
{
    User? GetUserByEmail(string email);
}
