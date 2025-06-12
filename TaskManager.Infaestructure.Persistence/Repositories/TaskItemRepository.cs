using Core.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Repositories;
using TaskManager.Infaestructure.Persistence.Context;
using TaskManager.Infaestructure.Persistence.Repositories.Base;

namespace TaskManager.Infaestructure.Persistence.Repositories
{
    public class TaskItemRepository : GenericRepository<TaskItem>, ITaskItemRepository
    {
        private readonly ApplicationContext _context;

        public TaskItemRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync(GetAllTaskQuery query, CancellationToken cancellationToken)
        {
            var tasks = _context.Tasks
                .AsNoTracking()
                .AsQueryable();

            if (query.StatusTask.HasValue)
                tasks = tasks.Where(t => t.Status == query.StatusTask);

            if (query.TaskType.HasValue)
                tasks = tasks.Where(t => t.Type == query.TaskType);

            return await tasks.ToListAsync(cancellationToken);
        }
    }
}
