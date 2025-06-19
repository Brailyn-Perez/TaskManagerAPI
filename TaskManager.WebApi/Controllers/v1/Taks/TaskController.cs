using Core.Domain.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand;
using TaskManager.Core.Application.Features.TaskItem.Commands.DeleteTaskCommand;
using TaskManager.Core.Application.Features.TaskItem.Commands.UpdateTaskCommand;
using TaskManager.Core.Application.Features.TaskItem.Queries.GetAllTask;
using TaskManager.Core.Application.Features.TaskItem.Queries.GetTaskById;
using TaskManager.Core.Domain.Delegates;
using TaskManager.Core.Domain.Enums;
using TaskManager.WebApi.Controllers.v1.Base;

namespace TaskManager.WebApi.Controllers.v1.Taks
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var validator = new ValidateTaskDelegate();

            validator.OnTaskCreated = (task) =>
            {
                var diasRestantes = validator.CalculateDaysRemaining(task);
                Console.WriteLine($"Tarea creada: {task.Description}, vencimiento en {diasRestantes} día(s).");
            };

            var isValid = await validator.ValidateTaskAsync(new Core.Domain.Entities.TaskItem
            {
                Description = command.Description,
                DueDate = command.DueDate,
            }, message =>
            {
                Console.WriteLine($"Validación fallida: {message}");
            });

            if (!isValid)
            {
                return BadRequest("Datos inválidos para crear la tarea.");
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var record = new GetTaskById { Id = id };
            var result = await Mediator.Send(record);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Task ID mismatch.");
            }
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var command = new DeleteTaskCommand { Id = id };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks([FromQuery] GetAllTaskQuery filterQuery, CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(new GetAllTask()
            {
                query = filterQuery

            }, cancellationToken);
            return Ok(result);
        }
    }
}
