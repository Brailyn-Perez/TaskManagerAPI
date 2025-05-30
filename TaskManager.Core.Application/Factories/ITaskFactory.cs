using TaskManager.Core.Domain.Entities.Types;
using TaskManager.Core.Domain.Enums;

namespace TaskManager.Core.Application.Factories
{
    public interface ITaskFactory
    {
        BaseTaskItem CreateTask(TaskType type, string description, DateTime dueDate);
    }

}
