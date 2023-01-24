using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IImageRepository : IDisposable
{
    Task Add(IList<Image> images);
}
