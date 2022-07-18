using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IUserRepository : IDisposable
{
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}
