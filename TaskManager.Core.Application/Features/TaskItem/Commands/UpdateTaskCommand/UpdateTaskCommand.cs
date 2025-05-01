using MediatR;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.UpdateTaskCommand
{
    public class UpdateTaskCommand : IRequest<Response<int>>
    {
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Response<int>>
    {
        public readonly ITaskItemRepository _repository;

        public UpdateTaskCommandHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public Task<Response<int>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
