using System.Security.Claims;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SkamBook.Application.Commands.Match;
using SkamBook.Application.Queries.Match.FetchNearest;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Context;
using SkamBook.Infrastructure.Interfaces;

namespace SkamBook.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IGoogleService _googleService;

    public MatchController(IMediator mediator, IGoogleService googleService)
    {
        _mediator = mediator;
        _googleService = googleService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> MatchUsersAsync([FromBody] MatchCommand matchCommand)
    {
        var response = await _mediator.Send(matchCommand);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}
