using AutoMapper;
using MediatR;
using System.Text.Json;
using TaskManager.Core.Application.Interfaces;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Enums;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.UpdateTaskCommand
{
    public class UpdateTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public StatusTask Status { get; set; }
        public DateTime DueDate { get; set; }
        public string? AditionalData { get; set; }
        public TaskType TaskType { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Response<int>>
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly IQueueTaskItemService _queueTaskItemService;

        public UpdateTaskCommandHandler(ITaskItemRepository repository, IMapper mapper, IQueueTaskItemService queueTaskItemService)
        {
            _repository = repository;
            _mapper = mapper;
            _queueTaskItemService = queueTaskItemService;
        }

        public async Task<Response<int>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (taskItem == null)
            {
                throw new KeyNotFoundException($"Task with ID {request.Id} not found.");
            }

            if (!string.IsNullOrEmpty(request.AditionalData))
            {
                string json = JsonSerializer.Serialize(request.AditionalData);
                request.AditionalData = json;
            }
            taskItem.Description = request.Description;
            taskItem.Status = request.Status;
            taskItem.DueDate = request.DueDate;
            taskItem.AditionalData = request.AditionalData;
            taskItem.Type = request.TaskType;

            var tcs = new TaskCompletionSource<int>();

            _queueTaskItemService.AddTaskItem(taskItem, async () =>
            {
                await _repository.UpdateAsync(taskItem, cancellationToken);
                tcs.SetResult(taskItem.Id);
            }, cancellationToken);

            int result = await tcs.Task;
            return new Response<int>(result, "Task encolada y actualizada exitosamente.");
        }
    }
}
