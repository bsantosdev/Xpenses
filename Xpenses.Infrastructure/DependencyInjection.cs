using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xpenses.Application.Common.Interfaces.Authentication;
using Xpenses.Application.Common.Interfaces.Persistence;
using Xpenses.Application.Common.Interfaces.Services;
using Xpenses.Infrastructure.Authentication;
using Xpenses.Infrastructure.Persistence;
using Xpenses.Infrastructure.Services;

namespace Xpenses.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}

