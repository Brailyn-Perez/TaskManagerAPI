using MediatR;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.DeleteTaskCommand
{
    public class DeleteTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Response<int>>
    {
        private readonly ITaskItemRepository _repository;

        public DeleteTaskCommandHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var Record = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (Record == null)
            {
                throw new KeyNotFoundException($"Task with id {request.Id} not found.");
            }

            await _repository.DeleteAsync(Record, cancellationToken);
            return new Response<int>(request.Id, "Task deleted successfully.");
        }
    }
}
