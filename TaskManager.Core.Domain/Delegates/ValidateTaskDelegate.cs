using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Enums;

namespace TaskManager.Core.Domain.Delegates
{
    public class ValidateTaskDelegate
    {
        public delegate bool ValidateTask(TaskItem taskItem);

        public async Task<bool> ValidateTaskAsync(TaskItem task)
        {
            ValidateTask validateTask = (TaskItem taskItem) =>
                !string.IsNullOrWhiteSpace(taskItem.Description) &&
                taskItem.DueDate > DateTime.Now &&
                (taskItem.Status == StatusTask.Pending || taskItem.Status == StatusTask.InProgress);

            return await Task.FromResult(validateTask(task));
        }
    }
}
