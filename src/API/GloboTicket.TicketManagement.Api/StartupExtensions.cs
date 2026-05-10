using GloboTicket.TicketManagement.Application;
using GloboTicket.TicketManagement.Application.Contracts;
using GloboTicket.TicketManagement.Infrastructure;
using GloboTicket.TicketManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Security.Claims;

using GloboTicket.TicketManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace GloboTicket.TicketManagement.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();

        builder.Services.AddCors(
                options => options.AddPolicy(
                    "open", policy =>
                    policy.WithOrigins([
                    builder.Configuration["ApiUrl"] ?? "https://localhost:7081",
                    builder.Configuration["BlazorUrl"] ?? "https://localhost:7080"])
            .AllowAnyMethod()
            .SetIsOriginAllowed(pol => true)
            .AllowAnyHeader()
            .AllowCredentials()));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.MapIdentityApi<ApplicationUser>();
        app.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.Ok();
});
        app.UseCors("open");
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        app.UseHttpsRedirection();
        // app.UseAuthorization();
        app.MapControllers();
        return app;
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetService<GloboTicketDbContext>();
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception)
        {
            // add loggin here
        }

    }

}
