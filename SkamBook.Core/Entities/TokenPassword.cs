namespace SkamBook.Core.Entities;

public class TokenPassword : BaseEntity
{
    public TokenPassword(string email, string token, string randomPassword)
    {
        Email = email;
        Token = token;
        RandomPassword = randomPassword;
    }

    public string Email { get; private set; }
    public string Token { get; private set; }
    public string RandomPassword { get; private set; }
}
