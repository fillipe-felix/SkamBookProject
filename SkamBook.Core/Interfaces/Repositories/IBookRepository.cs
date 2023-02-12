using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IBookRepository : IDisposable
{
    Task Add(Book book);
    Task<Book> GetBookByIdAsync(Guid id);
    
    Task<IEnumerable<Book>> GetAllBooksAsync();
    
    Task<IList<Book>> GetAllBooksToFetchNearestAsync(Guid userId, string addressCity);

    Task<IEnumerable<Guid>> GetBooksLikedIdById(Guid userId);
    Task<IList<Book>> GetAllBookById(IEnumerable<Guid> booksId);
}
