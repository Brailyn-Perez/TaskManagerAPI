using AutoMapper;
using MediatR;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand
{
    public class CreateTaskCommand : IRequest<Response<int>>
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
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
            var Record = _mapper.Map<CreateTaskCommand, Domain.Entities.TaskItem>(request);
            var result = await _repository.AddAsync(Record);

            return new Response<int>(result, "Task created successfully");
        }
    }
}
