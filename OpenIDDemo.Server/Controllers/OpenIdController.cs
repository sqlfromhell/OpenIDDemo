namespace OpenIDDemo.Server.Controllers;

[Route("connect")]
[ApiController]
public class OpenIdController
    : ControllerBase
{
    private const string TOKEN =
        CookieAuthenticationDefaults.AuthenticationScheme;

    private readonly TokenSettings TokenSettings;
    private readonly IUserRepository UserRepository;

    public OpenIdController(
        TokenSettings tokenSettings,
        IUserRepository userRepository
    )
    {
        TokenSettings = tokenSettings;
        UserRepository = userRepository;
    }

    [HttpGet("authorize")]
    public IActionResult Authorize
        ([FromQuery] OpenIdAuthorizeRequest request)
    {
        var user = UserRepository
            .Get(request.Username, request.Password);

        if (user is null)
            return Unauthorized();

        var returnUrl = TokenHelper
            .GetAuthorizeReturnUrl(
                request.RedirectUri,
                user.PublicId,
                request.State
            );

        return Redirect(returnUrl);
    }

    [HttpGet("logout")]
    public IActionResult Logout
        ([FromQuery] string postLogoutRedirectUri)
    {
        var redirectUrl =
            string.IsNullOrEmpty(postLogoutRedirectUri)
                ? "/"
                : postLogoutRedirectUri;

        return Redirect(redirectUrl);
    }

    [HttpPost("token")]
    public IActionResult Token
        ([FromForm] OpenIdTokenRequest model)
    {
        var user = UserRepository
            .Get(model.UserName, model.Password);

        return user is null
            ? Unauthorized()
            : Ok(new OpenIdTokenResponse()
            {
                AccessToken = TokenHelper
                    .Get(TokenSettings, user.GetPrincipal(TOKEN)),
                ExpiresIn =
                    TokenSettings.Expires
            });
    }

    [HttpGet("userinfo")]
    public IActionResult UserInfo
        ([FromQuery] string accessToken)
    {
        var principal =
            TokenSettings
                .GetPrincipal(accessToken);

        if (principal is null)
            return Unauthorized();

        var userInfo =
            principal.Claims
                .ToDictionary(e => e.Type, e => e.Value);

        return Ok(userInfo);
    }
}