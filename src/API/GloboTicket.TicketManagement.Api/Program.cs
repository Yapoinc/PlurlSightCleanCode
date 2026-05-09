using GloboTicket.TicketManagement.Api;
using Serilog;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
    Log.Information("Starting up the GloboTicket Ticket Management API");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) =>
    configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(),
    true
    
    );


var app = builder
.ConfigureServices()
.ConfigurePipeline();
app.UseSerilogRequestLogging();
await app.ResetDatabaseAsync();

app.Run();
