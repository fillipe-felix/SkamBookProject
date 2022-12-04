using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface ITokenPasswordRepository
{
    Task Add(TokenPassword tokenPassword);
    Task<TokenPassword> FindTokenPasswordByRandomPassword(string randomToken);
    Task<TokenPassword> FindTokenPasswordByEmail(string emai);
    Task Delete(TokenPassword tokenPassword);
}
