using System.Security.Claims;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SkamBook.Application;
using SkamBook.Application.Commands.Book.CreateBook;
using SkamBook.Application.Commands.UserEntity.CreateUser;
using SkamBook.Application.Queries.Match.FetchNearest;

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

    [Authorize]
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
    
    [HttpPost("book")]
    public async Task<IActionResult> AddBookAsync([FromBody] CreateBookCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("nearest-users")]
    public async Task<IActionResult> FetchNearestAsync()
    {
        var query = new FetchNearestQuery();
        
        var response = await _mediator.Send(query);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}