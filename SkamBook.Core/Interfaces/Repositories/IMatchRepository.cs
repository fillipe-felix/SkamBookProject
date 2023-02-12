using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IMatchRepository
{
    Task<MatchBook> GetMatchByIdBooks(Guid book1Id, Guid likedBookId);
    Task<IEnumerable<MatchBook>> GetLikesUserById(Guid userId);
    Task AddMatchBookAsync(MatchBook matchBook);
    Task UpdateMatchBookAsync(MatchBook match);
}
