using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IBookRepository : IDisposable
{
    Task Add(Book book);
    Task<Book> GetBookByIdAsync(Guid id);
}
