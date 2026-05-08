using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileVm>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(IMapper mapper, IEventRepository eventRepository, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _csvExporter = csvExporter;
        }

        public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = 1;
            var pageSize = 100;
            List<EventExportDto> allEvents = new List<EventExportDto>();
            do
            {
                List<Event> data = (await _eventRepository
                    .ListAllOrderByDateAsync(pageNumber, pageSize))
                  .ToList();
                if (data == null || data.Count == 0)
                    break;
                var mappedData = _mapper.Map<List<EventExportDto>>(data);
                allEvents.AddRange(mappedData);
                pageNumber++;
            }
            while (true);
            var fileData = _csvExporter.ExportEventsToCsv(allEvents);
            var eventExportFileDto = new EventExportFileVm() { ContentType = "text/csv", Data = fileData, EventExportFileName = $"{Guid.NewGuid()}.csv" };
            return eventExportFileDto;
        }
    }
}
