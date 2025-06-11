using Core.Domain.Shared;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Domain.Repositories
{
    public interface ITaskItemRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetAllAsync(GetAllTaskQuery query, CancellationToken cancellationToken);
    }
}
