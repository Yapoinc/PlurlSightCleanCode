using MediatR;
namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent;

public class UpdateEventCommand : IRequest
{
    public required Guid EventId { get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public required string Artist { get; set; }
    public required DateTime Date { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required Guid CategoryId { get; set; }
}
