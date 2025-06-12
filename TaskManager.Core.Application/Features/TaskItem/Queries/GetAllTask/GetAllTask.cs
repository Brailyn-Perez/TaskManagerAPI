using Core.Domain.Shared;
using MediatR;
using TaskManager.Core.Application.DTOs.TaskItem;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Queries.GetAllTask
{
    public class GetAllTask : IRequest<Response<IEnumerable<TaskItemDTO>>>
    {
        public GetAllTaskQuery query { get; set; }
    }

    public class GetAllTaskHandler : IRequestHandler<GetAllTask, Response<IEnumerable<TaskItemDTO>>>
    {
        private readonly ITaskItemRepository _repository;
        private static readonly Dictionary<string, IEnumerable<TaskItemDTO>> _cache = new();

        public GetAllTaskHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<TaskItemDTO>>> Handle(GetAllTask request, CancellationToken cancellationToken)
        {
            string cacheKey = GenerateCacheKey(request.query);

            if(_cache.TryGetValue(cacheKey, out var cachedResult))
            {
                return new Response<IEnumerable<TaskItemDTO>>(cachedResult, "Tasks retrieved from cache.");
            }

            var taskItems = await _repository.GetAllAsync(request.query, cancellationToken);
            if (taskItems == null)
            {
                throw new KeyNotFoundException("No tasks found.");
            }
            var taskItemDTOs = taskItems.Select(taskItem => new TaskItemDTO
            {
                Id = taskItem.Id,
                Description = taskItem.Description,
                Status = taskItem.Status,
                DueDate = taskItem.DueDate,
                AditionalData = taskItem.AditionalData,
                TaskType = taskItem.Type
            });


            _cache[cacheKey] = taskItemDTOs;

            return new Response<IEnumerable<TaskItemDTO>>(taskItemDTOs, "Tasks retrieved successfully.");
        }

        private string GenerateCacheKey(GetAllTaskQuery query)
        {
            var typePart = query.TaskType?.ToString() ?? "AllTypes";
            var statusPart = query.StatusTask?.ToString() ?? "AllStatus";
            return $"{typePart}_{statusPart}";
        }
    }
}
