namespace OpenIDDemo.Server.Models;

public class User
{
    public int Id { get; set; }
    public string Password { get; set; }

    public Guid PublicId { get; set; }
        = Guid.NewGuid();

    public string UserName { get; set; }

    internal ClaimsPrincipal GetPrincipal
        (string token)
    {
        List<Claim> claims = new()
        {
            new (ClaimTypes.Name, UserName),
        };

        ClaimsIdentity identity =
            new(claims, token);

        return
            new(identity);
    }
}