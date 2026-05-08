using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;

namespace GloboTicket.TicketManagement.Persistence.Repositories;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(GloboTicketDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate)
    {
        var matches = _dbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Date.Equals(eventDate.Date));
        return Task.FromResult(matches);
    }

    public Task<IReadOnlyList<Event>> ListAllOrderByDateAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var rows = pageSize;
        var data = _dbContext.Events.OrderBy(x => x.Date).Skip(skip).Take(rows).ToList();
        return Task.FromResult((IReadOnlyList<Event>)data);
    }
}

