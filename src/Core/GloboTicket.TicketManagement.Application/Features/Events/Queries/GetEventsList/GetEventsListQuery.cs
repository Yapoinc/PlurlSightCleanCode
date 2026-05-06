using MediatR;
using Microsoft.IdentityModel.Tokens;
namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList;

public class GetEventsListQuery: IRequest<List<EventListVm>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;

}
