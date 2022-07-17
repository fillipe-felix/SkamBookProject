using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Context;
using SkamBook.Infrastructure.UI;

namespace SkamBook.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly ApiSettings _apiSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private const string ROLE = "role";


    public JwtService(ApiSettings apiSettings, UserManager<ApplicationUser> userManager)
    {
        _apiSettings = apiSettings;
        _userManager = userManager;
    }

    public async Task<string> GenerateJwtToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        var claims = await CreateClaims(user);

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var encodedToken = EncodedToken(identityClaims);

        return encodedToken;
    }

    
    private string EncodedToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_apiSettings.JwtSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _apiSettings.JwtSettings.ValidIssuer,
            Audience = _apiSettings.JwtSettings.ValidAudience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_apiSettings.JwtSettings.Expiration)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);
        
        return encodedToken;
    }
    
    private async Task<IList<Claim>> CreateClaims(ApplicationUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ROLE, role));
        }
        
        return claims;
    }
    
    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
