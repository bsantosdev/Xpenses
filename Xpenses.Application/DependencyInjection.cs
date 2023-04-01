using Microsoft.Extensions.DependencyInjection;
using Xpenses.Application.Services.Authentication;

namespace Xpenses.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}

