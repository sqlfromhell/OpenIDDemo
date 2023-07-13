namespace OpenIDDemo.Base.Models;

public class TokenSettings
{
    public string Audience { get; set; }
    public int Expires { get; set; }
    public string Issuer { get; set; }
    public string SecretKey { get; set; }
    public string Service { get; set; }

    public TokenValidationParameters GetParameters()
    {
        SymmetricSecurityKey key = new
            (Encoding.UTF8.GetBytes(SecretKey));

        return new()
        {
            ValidateIssuer = Issuer is not null,
            ValidateAudience = Audience is not null,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            RequireExpirationTime = true,
            IssuerSigningKey = key,
        };
    }

    public ClaimsPrincipal GetPrincipal
        (string token)
    {
        try
        {
            JwtSecurityTokenHandler handler = new();

            var jwtToken = (JwtSecurityToken)handler
                .ReadToken(token);

            return jwtToken is null
                ? null
                : handler.ValidateToken
                    (token, GetParameters(), out _);
        }
        catch
        {
            return null;
        }
    }
}