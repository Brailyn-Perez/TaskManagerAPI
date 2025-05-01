using MediatR;
using TaskManager.Core.Application.DTOs.TaskItem;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Queries.GetAllTask
{
    public class GetAllTask : IRequest<Response<IEnumerable<TaskItemDTO>>>
    {

    }

    public class GetAllTaskHandler : IRequestHandler<GetAllTask, Response<IEnumerable<TaskItemDTO>>>
    {
        private readonly ITaskItemRepository _repository;

        public GetAllTaskHandler(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public Task<Response<IEnumerable<TaskItemDTO>>> Handle(GetAllTask request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
