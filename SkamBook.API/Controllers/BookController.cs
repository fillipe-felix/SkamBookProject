using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SkamBook.Application.Queries.BookQuery.BooksLiked;
using SkamBook.Application.ViewModels;
using SkamBook.Core.Interfaces.Repositories;

namespace SkamBook.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IMediator _mediator;


    public BookController(IBookRepository bookRepository, IMediator mediator)
    {
        _bookRepository = bookRepository;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IList<BookViewModel>), 200)]
    [ProducesResponseType(typeof(IList<string>), 400)]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookRepository.GetAllBooksAsync();

        if (books is null)
        {
            return NotFound();
        }

        return Ok(books);
    }
    
    [HttpGet("liked")]
    [Authorize]
    public async Task<IActionResult> GetBooksLiked()
    {
        var query = new BooksLikedQuery();
        
        var response = await _mediator.Send(query);

        if (response.Success.Equals(false))
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}
