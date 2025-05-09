using MediatR;
using TaskManager.Core.Application.DTOs.TaskItem;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Queries.GetTaskById
{
    public class GetTaskById : IRequest<Response<TaskItemDTO>>
    {
        public int Id { get; set; }
    }

    public class GetTaskByIdHandler : IRequestHandler<GetTaskById, Response<TaskItemDTO>>
    {
        private readonly ITaskItemRepository _repository;

        public GetTaskByIdHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<TaskItemDTO>> Handle(GetTaskById request, CancellationToken cancellationToken)
        {
            var taskItem = await _repository.GetByIdAsync(request.Id);
            if (taskItem == null)
            {
                throw new KeyNotFoundException($"Task with ID {request.Id} not found.");
            }
            var taskItemDTO = new TaskItemDTO
            {
                Id = taskItem.Id,
                Description = taskItem.Description,
                Status = taskItem.Status,
                DueDate = taskItem.DueDate,
                IsCompleted = taskItem.IsCompleted,
                AditionalData = taskItem.AditionalData
            };
            return new Response<TaskItemDTO>(taskItemDTO, "Task retrieved successfully.");
        }
    }
}
