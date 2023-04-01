using Xpenses.Application.Common.Interfaces.Services;

namespace Xpenses.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
