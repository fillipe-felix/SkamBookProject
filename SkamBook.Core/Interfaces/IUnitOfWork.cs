namespace SkamBook.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
    Task Rollback();
}
