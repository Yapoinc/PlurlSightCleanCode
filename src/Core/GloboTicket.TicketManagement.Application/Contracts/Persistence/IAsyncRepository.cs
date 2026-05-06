using System.Linq.Expressions;

namespace GloboTicket.TicketManagement.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync(int skip, int rows, Expression<Func<T, object>>? orderBy = null);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

}
