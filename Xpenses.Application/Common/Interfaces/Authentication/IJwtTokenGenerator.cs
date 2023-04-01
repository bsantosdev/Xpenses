using Xpenses.Domain.Entities;

namespace Xpenses.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}

