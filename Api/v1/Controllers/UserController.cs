using CrmCore.Core.Application.User.Commands.CreateUser;
using CrmCore.Core.Application.User.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;

namespace CrmCore.Api.v1.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly CreateUserCommandHandler _create;
    private readonly GetUserByIdQueryHandler _get;

    public UserController(
        CreateUserCommandHandler create,
        GetUserByIdQueryHandler get
    )
    {
        _create = create;
        _get = get;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand cmd)
    {
        try
        {
            var id = await _create.Handle(cmd);
            return Ok(new { id });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (System.Exception)
        {
            return BadRequest("User create Failed");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _get.Handle(new GetUserByIdQuery(id));
        return user is null ? NotFound() : Ok(user);
    }
}