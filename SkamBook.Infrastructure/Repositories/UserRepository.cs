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

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Email.Endereco.Equals(email));

        return user;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
