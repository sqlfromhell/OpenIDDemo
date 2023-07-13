namespace OpenIDDemo.Server.Controllers;

[Route(".well-known")]
[ApiController]
public class WellKnownController
    : ControllerBase
{
    private readonly TokenSettings TokenSettings;

    public WellKnownController
        (TokenSettings tokenSettings)
        => TokenSettings = tokenSettings;

    [HttpGet("openid-configuration")]
    public IActionResult OpenIdConfiguration()
    {
        var issuer = TokenSettings.Issuer;

        var openIdConfiguration = new OpenIdConfiguration
        {
            Issuer = issuer,
            AuthorizationEndpoint =
                $"{issuer}/connect/authorize",
            TokenEndpoint =
                $"{issuer}/connect/token",
            UserInfoEndpoint =
                $"{issuer}/connect/userinfo",
            LogoutEndpoint =
                $"{issuer}/connect/logout"
        };

        return Ok(openIdConfiguration);
    }
}