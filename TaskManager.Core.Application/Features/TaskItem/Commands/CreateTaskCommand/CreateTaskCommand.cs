﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using TaskManager.Core.Application.Factories;
using TaskManager.Core.Application.Interfaces;
using TaskManager.Core.Application.Wrapper;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Enums;
using TaskManager.Core.Domain.Repositories;

namespace TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand
{
    public class CreateTaskCommand : IRequest<Response<int>>
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskType TaskType { get; set; }
        public string? AditionalData { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Response<int>>
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITaskFactory _taskFactory;
        private readonly IQueueTaskItemService _queueTaskItemService;
        private readonly IHubContext<Hubs.Notifications> _hubContext;
        public CreateTaskCommandHandler(ITaskItemRepository repository, IMapper mapper, ITaskFactory taskFactory, IQueueTaskItemService queueTaskItemService, IHubContext<Hubs.Notifications> hubContext)
        {
            _repository = repository;
            _mapper = mapper;
            _taskFactory = taskFactory;
            _queueTaskItemService = queueTaskItemService;
            _hubContext = hubContext;
        }

        public async Task<Response<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.AditionalData))
            {
                string json = JsonSerializer.Serialize(request.AditionalData);
                request.AditionalData = json;
            }
            Domain.Entities.TaskItem taskItem = new Domain.Entities.TaskItem
            {
                Description = request.Description,
                DueDate = request.DueDate,
                Type = request.TaskType,
                AditionalData = request.AditionalData
            };
            var task = _taskFactory.CreateTask(taskItem);
            var tcs = new TaskCompletionSource<int>();

            _queueTaskItemService.AddTaskItem(task, async () =>
            {
                var createdId = await _repository.AddAsync(taskItem, cancellationToken);
                tcs.SetResult(createdId);
            }, cancellationToken);


            int result = await tcs.Task;
            await _hubContext.Clients.All.SendAsync("Task created successfully", result);
            return new Response<int>(result, "Task created successfully");
        }
    }
}
