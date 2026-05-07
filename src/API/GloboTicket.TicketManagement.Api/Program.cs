using GloboTicket.TicketManagement.Api;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder
.ConfigureServices()
.ConfigurePipeline();



// Configure the HTTP request pipeline.


// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

app.Run();
