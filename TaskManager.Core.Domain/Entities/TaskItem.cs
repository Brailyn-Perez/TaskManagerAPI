using TaskManager.Core.Domain.Common;
using TaskManager.Core.Domain.Enums;

namespace TaskManager.Core.Domain.Entities
{
    public class TaskItem : AuditableEntity
    {
        public string Description { get; set; } = string.Empty;
        public StatusTask Status { get; set; }
        public DateTime DueDate { get; set; }
        public string? AditionalData { get; set; }
    }
}
