using GloboTicket.TicketManagement.Application;
using GloboTicket.TicketManagement.Infrastructure;
using GloboTicket.TicketManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
// using GloboTicket.TicketManagement.Identity;
// using GloboTicket.TicketManagement.Identity.Models;

namespace GloboTicket.TicketManagement.Api;

public static class StartupExtensions
{
    public static void AddOpenApi(this IServiceCollection services)
    {

    }

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
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
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCors("open");
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        // app.UseHttpsRedirection();
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
        catch (Exception ex)
        {
           // add loggin here
        } 
        
    }

}
