using AutoMapper;
using MediatR;
using System.Text.Json;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Enums;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand
{
    public class CreateTaskCommand : IRequest<Response<int>>
    {
        public string Description { get; set; }
        public StatusTask Status { get; set; }
        public DateTime DueDate { get; set; }
        public string? AditionalData { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Response<int>>
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(ITaskItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.AditionalData))
            {
                string json = JsonSerializer.Serialize(request.AditionalData);
                request.AditionalData = json;
            }

            var Record = _mapper.Map<CreateTaskCommand, Domain.Entities.TaskItem>(request);
            var result = await _repository.AddAsync(Record,cancellationToken);

            return new Response<int>(result, "Task created successfully");
        }
    }
}
