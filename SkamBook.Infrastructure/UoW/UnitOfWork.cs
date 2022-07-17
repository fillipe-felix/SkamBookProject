using SkamBook.Core.Interfaces;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly SkamBookContext _context;

    public UnitOfWork(SkamBookContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Commit()
    {
        var success = (await _context.SaveChangesAsync()) > 0;

        return success;
    }

    public Task Rollback()
    {
        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
