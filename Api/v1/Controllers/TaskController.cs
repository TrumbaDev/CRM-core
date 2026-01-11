using CrmCore.Core.Application.Task.Commands.CreateTask;
using CrmCore.Core.Application.Task.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CrmCore.Api.v1.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(id));
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand cmd)
    {
        try
        {
            int id = await _mediator.Send(cmd);
            return Ok(new { id });
        }
        catch (Exception ex)
        {
            return BadRequest("Task create Failed: " + ex.Message);
        }
    }
}