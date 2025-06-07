using TaskManager.Core.Application.Factories;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Entities.Types;
using TaskManager.Core.Domain.Enums;

namespace TaskManager.Infaestructure.Persistence.Factories
{
    public class TaskFactory : ITaskFactory
    {
        public BaseTaskItem CreateTask(TaskItem taskItem)
        {
            BaseTaskItem task = taskItem.Type switch
            {
                TaskType.BugFix => new BugFixTask(),
                TaskType.Feature => new FeatureTask(),
                TaskType.Refactor => new RefactorTask(),
                _ => throw new NotSupportedException($"TaskType '{taskItem.Type}' not supported.")
            };

            task.Description = taskItem.Description;
            task.DueDate = taskItem.DueDate;
            task.Status = StatusTask.Pending;
            task.AditionalData = taskItem.AditionalData;
            task.Type = taskItem.Type;

            return task;
        }
    }
}
