using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly SkamBookContext _context;

    public ImageRepository(SkamBookContext context)
    {
        _context = context;
    }

    public async Task Add(IList<Image> images)
    {
        await _context.AddRangeAsync(images);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
