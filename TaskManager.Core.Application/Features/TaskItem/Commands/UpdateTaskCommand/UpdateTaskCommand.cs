using AutoMapper;
using MediatR;
using System.Text.Json;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.UpdateTaskCommand
{
    public class UpdateTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? AditionalData { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Response<int>>
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(ITaskItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _repository.GetByIdAsync(request.Id,cancellationToken);

            if (taskItem == null)
            {
                throw new KeyNotFoundException($"Task with ID {request.Id} not found.");
            }

            if (!string.IsNullOrEmpty(request.AditionalData))
            {
                string json = JsonSerializer.Serialize(request.AditionalData);
                request.AditionalData = json;
            }
            var newRecord = _mapper.Map(request, taskItem);

            await _repository.UpdateAsync(newRecord, cancellationToken);
            return new Response<int>(newRecord.Id, "Task updated successfully.");
        }
    }
}
