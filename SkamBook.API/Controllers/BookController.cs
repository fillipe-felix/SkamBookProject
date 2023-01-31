using Microsoft.AspNetCore.Mvc;

using SkamBook.Core.Interfaces.Repositories;

namespace SkamBook.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;


    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookRepository.GetAllBooksAsync();

        if (books is null)
        {
            return NotFound();
        }

        return Ok(books);
    }
}
