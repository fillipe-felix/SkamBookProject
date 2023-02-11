using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SkamBookContext _context;
    

    public UserRepository(SkamBookContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Email.Endereco.Equals(email));

        return user;
    }

    public async Task<User> GetUserByIdAsync(Guid UserId)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Id.Equals(UserId));

        return user;
    }

    public async Task<User> GetUserByEmailWithAddressAsync(string email)
    {
        var user = await _context
            .Users
            .Include(u => u.Address)
            .SingleOrDefaultAsync(u => u.Email.Endereco.Equals(email));

        return user;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.AddAsync(user);
    }

    public async Task<User> GetUserByEmailWithBooksAsync(string email)
    {
        var user = await _context
            .Users
            .Include(u => u.Books)
            .SingleOrDefaultAsync(u => u.Email.Endereco.Equals(email));

        return user;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
