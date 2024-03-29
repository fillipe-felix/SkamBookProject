using MediatR;

using Microsoft.AspNetCore.Mvc;

using SkamBook.Application.Commands.Authentication.RegisterUser;
using SkamBook.Application.Commands.ForgotPasswordUser;
using SkamBook.Application.Commands.ResetPasswordUser;
using SkamBook.Application.Commands.ValidRandomToken;
using SkamBook.Application.Queries.Authentication.LoginUser;
using SkamBook.Application.ViewModels;

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
    [ProducesResponseType(typeof(LoginResponseViewModel), 200)]
    [ProducesResponseType(typeof(IList<string>), 400)]
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
    [ProducesResponseType(typeof(LoginResponseViewModel), 200)]
    [ProducesResponseType(typeof(IList<string>), 400)]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        var response = await _mediator.Send(query);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordUserCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("reset-password")]
    [ProducesResponseType(typeof(LoginResponseViewModel), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("valid-code")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> ValidCodeAsync([FromBody] ValidRandomTokenCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}
