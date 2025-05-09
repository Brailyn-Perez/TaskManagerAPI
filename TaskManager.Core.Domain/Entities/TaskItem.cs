using TaskManager.Core.Domain.Common;

namespace TaskManager.Core.Domain.Entities
{
    public class TaskItem : AuditableEntity
    {
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? AditionalData { get; set; }
    }
}
