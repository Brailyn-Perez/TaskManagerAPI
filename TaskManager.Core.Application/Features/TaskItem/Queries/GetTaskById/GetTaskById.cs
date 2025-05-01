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

        public Task<Response<TaskItemDTO>> Handle(GetTaskById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
