using MediatR;

using Microsoft.AspNetCore.Mvc;

using SkamBook.Application.Commands.UserEntity.CreateUser;

namespace SkamBook.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}
