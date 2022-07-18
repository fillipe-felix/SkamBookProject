using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface ICategoryRepository : IDisposable
{
    Task Add(Category category);
    Task<IEnumerable<Category>> GetAllAsync();
}
