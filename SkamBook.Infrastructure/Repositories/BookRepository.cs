using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly SkamBookContext _context;

    public BookRepository(SkamBookContext context)
    {
        _context = context;
    }

    public async Task Add(Book book)
    {
        await _context.AddAsync(book);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
