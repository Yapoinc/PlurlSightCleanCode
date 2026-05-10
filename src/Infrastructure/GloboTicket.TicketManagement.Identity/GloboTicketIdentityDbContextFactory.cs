using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GloboTicket.TicketManagement.Identity;

public class GloboTicketIdentityDbContextFactory : IDesignTimeDbContextFactory<GloboTicketIdentityDbContext>
{
    public GloboTicketIdentityDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<GloboTicketIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("GloboTicketIdentityConnectionString");
        optionsBuilder.UseSqlServer(connectionString);

        return new GloboTicketIdentityDbContext(optionsBuilder.Options);
    }
}
