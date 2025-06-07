using TaskManager.Core.Domain.Enums;

namespace TaskManager.Core.Application.DTOs.TaskItem
{
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public StatusTask Status { get; set; }
        public DateTime DueDate { get; set; }
        public string AditionalData { get; set; }
        public TaskType TaskType { get; set; }
    }
}
