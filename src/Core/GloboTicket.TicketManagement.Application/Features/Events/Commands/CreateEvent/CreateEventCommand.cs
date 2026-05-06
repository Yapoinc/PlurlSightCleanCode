using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;
public class CreateEventCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required string Artist { get; set; }
        public required DateTime Date { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public required Guid CategoryId { get; set; }
        public override string ToString()
        {
            return $"Event name: {Name}; Price: {Price}; By: {Artist}; On: {Date.ToShortDateString()}; Description: {Description}";
        }
    }