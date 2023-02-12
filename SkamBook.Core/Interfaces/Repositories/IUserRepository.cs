using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IUserRepository : IDisposable
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(Guid UserId);
    Task<User> GetUserByEmailWithAddressAsync(string email);
    Task AddUserAsync(User user);
    Task<User> GetUserByEmailWithBooksAsync(string email);
    
    Task<IEnumerable<User>> GetUsersLikedBooks(Guid userId);
}
