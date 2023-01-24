using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.Repositories;

public class MatchReposotiry : IMatchRepository
{
    private readonly SkamBookContext _context;
    

    public MatchReposotiry(SkamBookContext context)
    {
        _context = context;
    }
    

    public async Task<MatchBook> GetMatchByIdBooks(Guid book1Id, Guid likedBookId)
    {
        var matches = await _context.Matches.FirstOrDefaultAsync(m =>
            (m.BookId == book1Id && m.BookIdLiked == likedBookId) ||
            (m.BookId == likedBookId && m.BookIdLiked == book1Id));

        return matches;
    }

    public async Task AddMatchBookAsync(MatchBook matchBook)
    {
        await _context.Matches.AddAsync(matchBook);
    }

    public async Task UpdateMatchBookAsync(MatchBook match)
    {
        _context.Matches.Update(match);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
