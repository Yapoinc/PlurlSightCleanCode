using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;

public class GetCategoriesListQuery : IRequest<List<CategoryListVm>>
{
    public GetCategoriesListQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
    public GetCategoriesListQuery()
    {
        Page = 1;
        PageSize = 50;
    }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

