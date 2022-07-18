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
    public async Task<IActionResult> AddCategoryAsync()
    {
        var category = new Category("Ação");
        await _categoryRepository.Add(category);
        await _unitOfWork.Commit();

        return Ok();
    }
}
