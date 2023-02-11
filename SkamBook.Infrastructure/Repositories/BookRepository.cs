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

    public async Task<IList<Book>> GetAllBooksToFetchNearestAsync(Guid userId, string addressCity)
    {
        var response = await _context.Books
           .Include(b => b.User.Address)
           .Include(b => b.BookImages)
           .ThenInclude(b => b.Image)
           .Include(b => b.LikedBy)
           .Include(b => b.User.ImageProfile)
           .Where(s => 
               (s.User.Address.City.Equals(addressCity) || s.User.Books.Count > 0) && 
               !s.User.Id.Equals(userId) 
               && s.LikedBy.All(lb => lb.BookIdLiked != s.Id))
           .ToListAsync();

       return response;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
