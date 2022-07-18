using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly SkamBookContext _context;

    public CategoryRepository(SkamBookContext context)
    {
        _context = context;
    }

    public async Task Add(Category category)
    {
        await _context.AddAsync(category);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
