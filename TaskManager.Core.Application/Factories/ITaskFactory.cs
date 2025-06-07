using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Entities.Types;
using TaskManager.Core.Domain.Enums;

namespace TaskManager.Core.Application.Factories
{
    public interface ITaskFactory
    {
        BaseTaskItem CreateTask(TaskItem task);
    }

}
