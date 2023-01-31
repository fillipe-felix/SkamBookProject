using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IUserRepository : IDisposable
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByEmailWithAddressAsync(string email);
    Task AddUserAsync(User user);
    Task<IList<User>> GetAllUserAsync();
    Task<IList<User>> GetAllUserByCityAddressWithBooksAsync(string city, string email);
    Task<IList<User>> GetAllUserWithAddressAndBooksAsync(string email);
    Task<User> GetUserByEmailWithBooksAsync(string email);
}
