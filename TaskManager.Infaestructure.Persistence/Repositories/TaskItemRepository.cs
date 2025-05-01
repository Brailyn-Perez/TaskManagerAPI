using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Repositories;
using TaskManager.Infaestructure.Persistence.Context;
using TaskManager.Infaestructure.Persistence.Repositories.Base;

namespace TaskManager.Infaestructure.Persistence.Repositories
{
    public class TaskItemRepository : GenericRepository<TaskItem> , ITaskItemRepository
    {
        private readonly ApplicationContext _context;

        public TaskItemRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
