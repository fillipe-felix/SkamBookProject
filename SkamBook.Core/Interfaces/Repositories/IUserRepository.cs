using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IUserRepository : IDisposable
{
    Task<User> GetUserByEmail(string email);
}
