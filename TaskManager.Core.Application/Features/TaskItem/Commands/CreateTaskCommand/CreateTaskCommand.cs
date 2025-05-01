using MediatR;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand
{
    public class CreateTaskCommand : IRequest<Response<int>>
    {

    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Response<int>>
    {
        private readonly ITaskItemRepository _repository;

        public CreateTaskCommandHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public Task<Response<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
