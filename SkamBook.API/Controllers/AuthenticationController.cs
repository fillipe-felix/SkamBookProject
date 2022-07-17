using MediatR;

using Microsoft.AspNetCore.Mvc;

using SkamBook.Application.Commands.Authentication.RegisterUser;
using SkamBook.Application.Queries.Authentication.LoginUser;
using SkamBook.Application.Queries.User.LoginUser;

namespace SkamBook.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        var response = await _mediator.Send(query);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}
