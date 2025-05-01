namespace TaskManager.Core.Application.DTOs.TaskItem
{
    public class TaskItemDTO
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
