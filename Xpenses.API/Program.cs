using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Xpenses.Api;
using Xpenses.Application;
using Xpenses.Infrastructure;

// Log expenses, tag them and add categories
// List of expenses per month
// Have a few graphs (month and year)
// Store them in a database
// Cache data with Redis or similar


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((ctx, lc) =>
    {
        var name = typeof(Program).Assembly.GetName().Name;

        lc.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information);
        lc.Enrich.FromLogContext();
        lc.Enrich.WithMachineName();
        lc.Enrich.WithProperty("Assembly", name);
        lc.WriteTo.Console();
        lc.WriteTo.File("Logs/logs.json", rollingInterval: RollingInterval.Day);
        lc.WriteTo.Seq(serverUrl: "http://host.docker.internal:5341");
    });

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services
        .Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

    builder.Services
        .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    var connString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services
        .AddDbContext<XpensesDbContext>(options => options
            .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Xpenses API",
            Description = "Xpenses API for provide some data to the client",
            Version = "v1"
        });
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<XpensesDbContext>();
            dbContext.Database.Migrate();
        }
    }

    app.UseExceptionHandler("/error");
    app.Map("/error", (HttpContext httpContext) =>
    {
        Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Results.Problem();
    });

    //app.UseCustomRequestLogging();
    app.UseHttpsRedirection();
    app.BuildCategoryEndpoints();
    app.BuildAuthenticationEndpoints();
    app.Run();
}