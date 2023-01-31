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
    
    public async Task<IList<User>> GetAllUserAsync()
    {
        return await _context
            .Users
            .Include(u => u.Address)
            .ToListAsync();
    }
    
    public async Task<IList<User>> GetAllUserByCityAddressWithBooksAsync(string city, string email)
    {
        return await _context
            .Users
            .Include(i => i.ImageProfile)
            .Include(user => user.Address)
            .Include(user => user.Books)
            .ThenInclude(books => books.BookImages)
            .ThenInclude(image => image.Image)
            .Where(s => s.Address.City.Equals(city) && !s.Email.Endereco.Equals(email) && s.Books.Count > 0)
            .ToListAsync();
    }
    
    public async Task<IList<User>> GetAllUserWithAddressAndBooksAsync(string email)
    {
        return await _context
            .Users
            .Include(i => i.ImageProfile)
            .Include(user => user.Address)
            .Include(user => user.Books)
            .ThenInclude(books => books.BookImages)
            .ThenInclude(image => image.Image)
            .Where(s => !s.Email.Endereco.Equals(email) && s.Books.Count > 0)
            .ToListAsync();
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
