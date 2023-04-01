using Xpenses.Domain.Entities;

namespace Xpenses.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);
