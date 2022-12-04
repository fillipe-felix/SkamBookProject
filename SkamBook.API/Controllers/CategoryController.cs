using MediatR;

using Microsoft.AspNetCore.Mvc;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;

namespace SkamBook.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IMediator mediator, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    

    [HttpPost]
    public async Task<IActionResult> AddCategoryAsync([FromBody] string category)
    {
        await _categoryRepository.Add(new Category(category));
        await _unitOfWork.Commit();

        return Ok();
    }
    
    
    
    [HttpGet]
    public async Task<IActionResult> GetCategoryAsync()
    {
        var response = await _categoryRepository.GetAllAsync();

        return Ok(response);
    }
}
