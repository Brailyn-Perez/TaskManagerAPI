using AutoMapper;
using TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand;
using TaskManager.Core.Application.Features.TaskItem.Commands.UpdateTaskCommand;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {

        public GeneralProfile()
        {
            CreateMap<TaskItem, CreateTaskCommand>();
            CreateMap<CreateTaskCommand, TaskItem>();
            CreateMap<TaskItem, UpdateTaskCommand>();
        }
    }
}
