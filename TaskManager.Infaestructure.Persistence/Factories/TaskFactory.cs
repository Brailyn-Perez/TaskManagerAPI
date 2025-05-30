using TaskManager.Core.Application.Factories;
using TaskManager.Core.Domain.Entities.Types;
using TaskManager.Core.Domain.Enums;

namespace TaskManager.Infaestructure.Persistence.Factories
{
    public class TaskFactory : ITaskFactory
    {
        public BaseTaskItem CreateTask(TaskType type, string description, DateTime dueDate)
        {
            BaseTaskItem task = type switch
            {
                TaskType.BugFix => new BugFixTask(),
                TaskType.Feature => new FeatureTask(),
                TaskType.Refactor => new RefactorTask(),
                _ => throw new NotSupportedException($"TaskType '{type}' not supported.")
            };

            task.Description = description;
            task.DueDate = dueDate;
            task.Status = StatusTask.Pending;

            return task;
        }
    }
}
