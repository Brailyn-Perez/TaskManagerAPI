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

        public GetAllTaskHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<TaskItemDTO>>> Handle(GetAllTask request, CancellationToken cancellationToken)
        {
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
            return new Response<IEnumerable<TaskItemDTO>>(taskItemDTOs, "Tasks retrieved successfully.");
        }
    }

}
