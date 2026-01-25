using CrmCore.Core.Application.User.Commands.CreateUser;
using CrmCore.Core.Application.User.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CrmCore.Core.Application.User.DTO;

namespace CrmCore.Api.v1.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand cmd)
    {
        try
        {
            int id = await _mediator.Send(cmd);
            return Ok(new { id });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest("User create failed: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        UserDTO? user = await _mediator.Send(new GetUserByIdQuery(id));
        return user is null ? NotFound() : Ok(user);
    }
}