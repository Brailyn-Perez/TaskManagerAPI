using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Domain.Common;
using TaskManager.Core.Domain.Repositories;
using TaskManager.Infaestructure.Persistence.Context;

namespace TaskManager.Infaestructure.Persistence.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : AuditableEntity
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.AddAsync<T>(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Update<T>(entity);
            await _context.SaveChangesAsync();
        }
    }
}
