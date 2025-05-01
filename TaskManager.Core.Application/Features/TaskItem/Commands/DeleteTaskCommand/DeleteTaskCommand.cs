using MediatR;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.DeleteTaskCommand
{
    public class DeleteTaskCommand : IRequest<Response<int>>
    {

    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Response<int>>
    {
        private readonly ITaskItemRepository _repository;

        public DeleteTaskCommandHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public Task<Response<int>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
