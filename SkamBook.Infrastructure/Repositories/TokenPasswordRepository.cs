using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.Repositories;

public class TokenPasswordRepository : ITokenPasswordRepository
{
    private readonly SkamBookContext _context;

    public TokenPasswordRepository(SkamBookContext context)
    {
        _context = context;
    }

    public async Task Add(TokenPassword tokenPassword)
    {
        await _context.AddAsync(tokenPassword);
    }

    public async Task<TokenPassword> FindTokenPasswordByRandomPassword(string randomToken)
    {
        var tokenPassword = await _context.TokenPasswords
            .SingleOrDefaultAsync(u => u.RandomPassword.Equals(randomToken));

        return tokenPassword;
    }

    public async Task<TokenPassword> FindTokenPasswordByEmail(string emai)
    {
        var tokenPassword = await _context.TokenPasswords
            .SingleOrDefaultAsync(u => u.Email.Equals(emai));

        return tokenPassword;
    }

    public async Task Delete(TokenPassword tokenPassword)
    {
        _context.TokenPasswords.Remove(tokenPassword);
    }
}
