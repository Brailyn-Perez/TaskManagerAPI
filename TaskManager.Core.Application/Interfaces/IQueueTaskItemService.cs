using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Application.Interfaces
{
    public interface IQueueTaskItemService
    {
        void AddTaskItem(TaskItem taskItem, Func<Task> action, CancellationToken cancellationToken);
    }
}
