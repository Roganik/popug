using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace popug.jwt;

public class JwtTokenService : IJwtTokenGenerator, IJwtTokenValidator
{
    private static string Secret = "PopugWasHere_2022_11_23";
    private static SecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    private static string Issuer = "Popug.SSO";

    public string Generate(Guid userId, string userName, string userRole)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName.ToString()),
                new Claim(ClaimTypes.Role, userRole.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = Issuer,
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool IsValid(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, CreateValidationParameters(), out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public TokenValidationParameters CreateValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = Issuer,
            IssuerSigningKey = SecurityKey
        };
    }

    public JwtSecurityTokenHandler GetValidator()
    {
        throw new NotImplementedException();
    }
}