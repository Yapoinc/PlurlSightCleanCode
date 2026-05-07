using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly GloboTicketDbContext _dbContext;
        protected readonly DbSet<T> _set;

        public BaseRepository(GloboTicketDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {            
            return await _set.FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> ListAllAsync(int skip, int rows)
        {
            return await _set.Skip(skip).Take(rows).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
