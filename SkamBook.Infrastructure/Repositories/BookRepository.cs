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

    public async Task<Book> GetBookByIdAsync(Guid id)
    {
        var book = await _context.Books
                    .SingleOrDefaultAsync(u => u.Id.Equals(id));
        
        return book;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        var books = await _context.Books.ToListAsync();
        
        return books;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
