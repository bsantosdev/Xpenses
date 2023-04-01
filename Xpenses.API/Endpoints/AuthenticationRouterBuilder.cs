using Xpenses.Application.Services.Authentication;
using Xpenses.Contracts.Authentication;

public static class AuthenticationRouterBuilder
{
    public static void BuildAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/authentication/register", Register);
        app.MapPost("/authentication/login", Login);
    }

    internal static IResult Register(
        IAuthenticationService authenticationService,
        RegisterRequest request
    )
    {
        var authResult = authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );

        return Results.Ok(response);
    }

    internal static IResult Login(
        IAuthenticationService authenticationService,
        LoginRequest request
    )
    {
        var authResult = authenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );

        return Results.Ok(response);
    }
}
