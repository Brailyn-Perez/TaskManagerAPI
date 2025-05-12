using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Domain.Delegates
{
    public class ValidateTaskDelegate
    {
    }
    
    public delegate bool ValidateTask(TaskItem taskItem);

}
