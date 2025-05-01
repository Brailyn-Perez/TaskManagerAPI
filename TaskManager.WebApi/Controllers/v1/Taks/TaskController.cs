using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Application.Features.TaskItem.Commands.CreateTaskCommand;
using TaskManager.WebApi.Controllers.v1.Base;

namespace TaskManager.WebApi.Controllers.v1.Taks
{
    public class TaskController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
