using TaskManager.Core.Domain.Enums;

namespace Core.Domain.Shared
{
    public class GetAllTaskQuery
    {
        public TaskType? TaskType { get; set; }
        public StatusTask? StatusTask { get; set; }
    }
}
