using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserOrg.BusinessLogic.Users.Commands;
using UserOrg.BusinessLogic.Users.Dtos;
using UserOrg.BusinessLogic.Users.Queries;
using UserOrg.Api.Auth;


namespace UserOrg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequireFirebaseAuth]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    // GET: api/users/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user is null) return NotFound();
        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserCommand command)
    {
        var created = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/users/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id) return BadRequest("ID in URL and body must match");

        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/users/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }
}
